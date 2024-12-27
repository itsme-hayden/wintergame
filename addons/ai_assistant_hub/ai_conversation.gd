@tool
class_name AIConversation

var _chat_history:= []
var _system_msg: String


func set_system_message(message:String) -> void:
	_system_msg = message
	# If your models don't mark the code with ```gdscript, the plugin won't wort well,
	# consider giving it an instruction like the one in the comment below, either in the
	# _system_msg or as part of the bot initial request.
	#
	#_system_msg = "%s. Any code you write you should identify with the programming language, for example for GDScript you must use prefix \"```gdscript\" and suffix \"```\"." % message
	#


func add_user_prompt(prompt:String) -> void:
	_chat_history.append(
		{
			"role": "user",
			"content": prompt
		}
	)


func add_assistant_response(response:String) -> void:
	_chat_history.append(
		{
			"role": "assistant",
			"content": response
		}
	)


func build() -> Array:
	var messages := []
	messages.append(
		{
			"role": "system",
			"content": _system_msg
		}
	)
	messages.append_array(_chat_history)
	return messages


func forget_last_prompt() -> void:
	_chat_history.pop_back()


func clone_chat() -> Array:
	return _chat_history.duplicate(true)


func overwrite_chat(new_chat:Array) -> void:
	_chat_history = new_chat
