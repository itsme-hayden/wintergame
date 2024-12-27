@tool
class_name LLMInterface
# The intention of this class is to serve as a base class for any LLM API
# to be implemented in this plugin. It is mainly to have a clear definition
# of what properties or functions should be used by other classes.

const INVALID_RESPONSE := "[INVALID_RESPONSE]"

var model: String
var override_temperature: bool
var temperature: float


func get_full_response(body:PackedByteArray) -> Dictionary:
	var json := JSON.new()
	json.parse(body.get_string_from_utf8())
	return json.get_data()


## All methods below should be overriden by child classes, see for example OllamaAPI

func send_get_models_request(http_request:HTTPRequest) -> bool:
	return false


func read_models_response(body:PackedByteArray) -> Array[String]:
	return [INVALID_RESPONSE]


func send_chat_request(http_request:HTTPRequest, content:Array) -> bool:
	return false


func read_response(body:PackedByteArray) -> String:
	return INVALID_RESPONSE
