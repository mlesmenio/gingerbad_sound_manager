# gingerbad_sound_manager

This asset is composed of two scripts ("SoundObject.cs", "SoundScript.cs") and their auxilliary code:

## SoundObject.cs

Generates a gameObject containing a "SoundScript" and its audioSource on runtime. Some fields are directly parsed ("audioClip", "outputMixer", "audioPriority", "loopAudio"), others were implemented for additional logic:

| Argument | Type | Required | Default | Description |
|----------|------|----------|---------|-------------|
| `ignoreTimescale` | Bool | ✓ | `F` | If false, the pitch is dynamically scaled with the value of the timescale |
| `baseVolume` | Float | ✓ | `0` | volume of the clip to be played |
| `randomVolume` | Float | No | `0` | volume = baseVolume + ] - randomVolume/2, randomVolume/2] |
| `basePitch` | Float | ✓ | `1` | pitch of the clip to be played |
| `randomPitch` | Float | No | `0` | pitch = basePitch + ] - randomPitch/2, randomPitch/2] |
| `dynamicVolume` | FloatVariable | No | `Null` | Allows dynamic updates to any audioSources currently playing |
| `settings3D` | gameObject | No | `Null` | Allows instantiating the gameObject as a copy of a prefab with at least an audioSource. The arguments included in "SoundObject" will be overwritten, the others will be kept as they are. The idea was to allow easily creating consistent volume curves across different sounds, as shown in the sample scene. Leave empty to generate 2D sounds.|

## SoundScript.cs

Script which handles the dynamic volume/pitch changes and the destruction of the generated gameObject after its audioSource finishes playing. If the audioSource contains a non-looping sound, the gameObject is destroyed automatically. If the audioSource contains a looping sound, the gameObject has to be handled with external calls.

## Usage

For a practical example on how to use this this asset, please consult the sample scene and the resources available there.

To play a clip create a SoundObject from the Unity asset menu and customize its fields. Then you can import this object to any script and call Play() or PlayFromUI() to generate audio at runtime:

### Play()

Can be called from any script. Accepts two optional parameters, returns a gameObject containing an audioSource and a SoundScript:

| Argument | Type | Required | Default | Description |
|----------|------|----------|---------|-------------|
| `parent` | Transform | No | `Null` | Allows instantiating the gameObject as a child of the given parent |
| `spawnPosition` | Vector3 | No | `Null` | If null, the gameObject will either be spawned at parent.position or at [0,0,0] |

### PlayFromUI()

Wrapper to allow calls from UI elements. Accepts no parameters and has no return value.
