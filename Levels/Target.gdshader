shader_type spatial;

uniform vec3 selectColor : source_color;
uniform sampler2D tex : source_color;
uniform float speed = 2.0;
uniform float height = 1.0;

void fragment() {
	vec3 color = texture(tex, UV).rgb;

	vec3 selectAnim = vec3(0, 0, 0);
	//selectAnim *= clamp(sin(speed * -TIME + VERTEX * 10.0) * height + 0.5, 0.0, 1.0);
	selectAnim += clamp(sin(TIME * speed) * 0.5 + 0.5, 0.0, 1.0);
	ALPHA = min(min(selectAnim.r, selectAnim.g), selectAnim.b);
	selectAnim *= selectColor;
	color *= selectAnim;
	
	
	ALBEDO = color;
}
