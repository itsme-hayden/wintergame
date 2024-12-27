@tool
class_name AIAssistantHub
extends Control

signal models_refreshed(models:Array[String])
signal new_api_loaded()

const NEW_AI_ASSISTANT_BUTTON = preload("res://addons/ai_assistant_hub/new_ai_assistant_button.tscn")

@onready var models_http_request: HTTPRequest = %ModelsHTTPRequest
@onready var url_txt: LineEdit = %UrlTxt
@onready var api_class_txt: LineEdit = %ApiClassTxt
@onready var models_list: RichTextLabel = %ModelsList
@onready var no_assistants_guide: Label = %NoAssistantsGuide
@onready var assistant_types_container: HFlowContainer = %AssistantTypesContainer
@onready var tab_container: TabContainer = %TabContainer

var _plugin:EditorPlugin
var _tab_bar:TabBar
var _model_names:Array[String] = []
var _models_llm: LLMInterface


func _tab_changed(tab_index: int) -> void:
	if tab_index > 0:
		_tab_bar.tab_close_display_policy = TabBar.CLOSE_BUTTON_SHOW_ACTIVE_ONLY
	else:
		_tab_bar.tab_close_display_policy = TabBar.CLOSE_BUTTON_SHOW_NEVER


func _close_tab(tab_index: int) -> void:
	var chat = tab_container.get_tab_control(tab_index)
	models_refreshed.disconnect(chat.refresh_models)
	chat.queue_free()


func initialize(plugin:EditorPlugin) -> void:
	_plugin = plugin
	_models_llm = _plugin.new_llm_provider()
	
	await ready
	url_txt.text = ProjectSettings.get_setting(AIHubPlugin.CONFIG_BASE_URL)
	api_class_txt.text = ProjectSettings.get_setting(AIHubPlugin.CONFIG_LLM_API)
	
	_on_assistants_refresh_btn_pressed()
	_on_refresh_models_btn_pressed()
	
	_tab_bar = tab_container.get_tab_bar()
	_tab_bar.tab_changed.connect(_tab_changed)
	_tab_bar.tab_close_pressed.connect(_close_tab)


func _on_settings_changed(_x) -> void:
	ProjectSettings.set_setting(AIHubPlugin.CONFIG_BASE_URL, url_txt.text)
	ProjectSettings.set_setting(AIHubPlugin.CONFIG_LLM_API, api_class_txt.text)


func _on_refresh_models_btn_pressed() -> void:
	models_list.text = ""
	_models_llm.send_get_models_request(models_http_request)


func _on_models_http_request_completed(result: HTTPRequest.Result, response_code: int, headers: PackedStringArray, body: PackedByteArray) -> void:
	if result == 0:
		var models_returned: Array = _models_llm.read_models_response(body)
		if models_returned.size() == 0:
			models_list.text = "No models found. Download at least one model and try again."
		else:
			if models_returned[0] == LLMInterface.INVALID_RESPONSE:
				models_list.text = "Error while trying to get the models list. Response: %s" % _models_llm.get_full_response(body)
			else:
				_model_names = models_returned
				for model in _model_names:
					models_list.text += "%s\n" % model
				models_refreshed.emit(_model_names) #for existing chats
	else:
		push_error("HTTP response: Result: %s, Response Code: %d, Headers: %s, Body: %s" % [result, response_code, headers, body])
		models_list.text = "Something went wrong querying for models, is the Server URL correct?"


func _on_assistants_refresh_btn_pressed() -> void:
	var assistants_path = "%s/assistants" % self.scene_file_path.get_base_dir()
	var files = _get_all_resources(assistants_path)
	var found:= false
	
	for child in assistant_types_container.get_children():
		if child != no_assistants_guide:
			assistant_types_container.remove_child(child)
	
	for assistant_file in files:
		var assistant = load(assistant_file)
		if assistant is AIAssistantResource:
			found = true
			var new_bot_btn:NewAIAssistantButton= NEW_AI_ASSISTANT_BUTTON.instantiate()
			new_bot_btn.initialize(_plugin, assistant)
			new_bot_btn.chat_created.connect(_on_new_bot_btn_chat_created)
			assistant_types_container.add_child(new_bot_btn)
	
	if not found:
		no_assistants_guide.text = "You have no assistant types! Create a new AIAssistantResource in the assistants folder, then click the refresh button. The folder is at: %s" % assistants_path
		no_assistants_guide.visible = true
		assistant_types_container.visible = false
	else:
		no_assistants_guide.visible = false
		assistant_types_container.visible = true


func _on_new_bot_btn_chat_created(chat:AIChat, assistant_type:AIAssistantResource) -> void:
	tab_container.add_child(chat)
	tab_container.set_tab_icon(tab_container.get_child_count() - 1, assistant_type.type_icon)
	chat.refresh_models(_model_names)
	models_refreshed.connect(chat.refresh_models)
	new_api_loaded.connect(chat.load_api)
	chat.greet()


func _get_all_resources(path: String) -> Array[String]:  
	var file_paths: Array[String] = []  
	var dir = DirAccess.open(path)  
	dir.list_dir_begin()  
	var file_name = dir.get_next()  
	while not file_name.is_empty():  
		if file_name.ends_with(".tres"):
			var file_path = path + "/" + file_name
			file_paths.append(file_path)  
		file_name = dir.get_next()
	return file_paths


func _on_api_load_btn_pressed() -> void:
	var new_llm:LLMInterface = _plugin.new_llm_provider()
	if new_llm == null:
		push_error("Invalid API class")
		return
	_models_llm = new_llm
	new_api_loaded.emit()
