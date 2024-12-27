@tool
class_name OllamaAPI
extends LLMInterface

const HEADERS := ["Content-Type: application/json"]

func send_get_models_request(http_request:HTTPRequest) -> bool:
	var url:String = "%s/api/tags" % ProjectSettings.get_setting(AIHubPlugin.CONFIG_BASE_URL)
	#print("Calling: %s" % url)
	var error = http_request.request(url, HEADERS, HTTPClient.METHOD_GET)
	if error != OK:
		push_error("Something when wrong with last AI API call: %s" % url)
		return false
	return true


func read_models_response(body:PackedByteArray) -> Array[String]:
	var json := JSON.new()
	json.parse(body.get_string_from_utf8())
	var response := json.get_data()
	if response.has("models"):
		var model_names:Array[String] = []
		for entry in response.models:
			model_names.append(entry.model)
		model_names.sort()
		return model_names
	else:
		return [INVALID_RESPONSE]


func send_chat_request(http_request:HTTPRequest, content:Array) -> bool:
	if model.is_empty():
		push_error("ERROR: You need to set an AI model for this assistant type.")
		return false
	
	var body_dict := {
		"messages": content,
		"stream": false,
		"model": model
	}
	
	if override_temperature:
		body_dict["options"] = { "temperature": temperature }
	
	var body := JSON.new().stringify(body_dict)
	
	var url = _get_chat_url()
	#print("calling %s with body: %s" % [url, body])
	var error = http_request.request(url, HEADERS, HTTPClient.METHOD_POST, body)
	if error != OK:
		push_error("Something when wrong with last AI API call.\nURL: %s\nBody:\n%s" % [url, body])
		return false
	return true


func read_response(body) -> String:
	var json := JSON.new()
	json.parse(body.get_string_from_utf8())
	var response := json.get_data()
	if response.has("message"):
		return response.message.content
	else:
		return LLMInterface.INVALID_RESPONSE


func _get_chat_url() -> String:
	return "%s/api/chat" % ProjectSettings.get_setting(AIHubPlugin.CONFIG_BASE_URL)
