@tool
class_name AIHubPlugin
extends EditorPlugin

const CONFIG_BASE_URL:= "ai_assistant_hub/base_url"
const CONFIG_LLM_API:= "ai_assistant_hub/llm_api"

var _hub_dock:AIAssistantHub

func _enter_tree() -> void:
	if ProjectSettings.get_setting(CONFIG_BASE_URL, "").is_empty():
		ProjectSettings.set_setting(CONFIG_BASE_URL, "http://127.0.0.1:11434")
	if ProjectSettings.get_setting(CONFIG_LLM_API, "").is_empty():
		ProjectSettings.set_setting(CONFIG_LLM_API, "ollama_api")
	
	_hub_dock = load("res://addons/ai_assistant_hub/ai_assistant_hub.tscn").instantiate()
	_hub_dock.initialize(self)
	add_control_to_bottom_panel(_hub_dock, "AI Hub")
	#add_control_to_dock(EditorPlugin.DOCK_SLOT_LEFT_UL, _hub_dock)


func _exit_tree() -> void:
	remove_control_from_bottom_panel(_hub_dock)
	#remove_control_from_docks(_hub_dock)
	_hub_dock.queue_free()


## Load the API dinamically based on the script name given in project setting: ai_assistant_hub/llm_api
## By default this is equivalent to: return OllamaAPI.new()
func new_llm_provider() -> LLMInterface:
	return load("res://addons/ai_assistant_hub/llm_apis/%s.gd" % ProjectSettings.get_setting(AIHubPlugin.CONFIG_LLM_API)).new()
