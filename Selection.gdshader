shader_type spatial;

uniform vec3 selectColor : source_color;
uniform sampler2D tex : source_color;
uniform bool selected = false;
//uniform float speed = 2.0;
//uniform float height = 1.0;

void fragment() {
	vec3 color = texture(tex, UV).rgb;
	if (selected) {
		vec3 selectAnim = vec3(1, 1, 1);
		//selectAnim *= clamp(sin(speed * -TIME + UV.y * 10.0) * height + 0.5, 0.0, 1.0);
		selectAnim *= selectColor;
		color *= selectAnim;
	}
	ALBEDO = color;
}
