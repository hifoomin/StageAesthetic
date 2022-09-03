## OVERVIEW
- All normal stages except for Commencement (so not including any Hidden Realms or other stages that don't advance the stage count once you clear them) will roll for either vanilla or a variant. Commencement has a single variant that will always be on. If desired, any color scheme (including the vanilla stage) can be disabled in config.
- If a variant is selected, changes are made to post-processing (mostly RampFog), global lighting, and (if needed) individual props to alter the stage's appearance. For example, the variants included for Titanic Plains are inspired from various trailers - an orange one from very early footage of the game, and an overcast one vaguely similar to the console trailer.
- Once a variant is used, it cannot be chosen for the next time the stage is loaded unless there are no other options.
- Some unused stage props (most notably the border statues in Abandoned Aqueduct) may be enabled.

## TODO
I don't have any plans to tackle these right now due to life stuff, but possibly coming sometime in the next decade:
- Reintroduce Artifact of Seasons once it's become more stable
- Look at compatibility with other mods
- Add more variants
- Update to DLC once that rolls in

I've taken down my modding server for now. If you have questions, message me on discord or use the github link above.

## KNOWN BUGS
- Light effects on the crystals in Night Distant Roost do not have shadows. This is vanilla behavior, and I've decided to leave it in to aid visibility
- Embers still appear in Night Scorched Acres despite it being raining. I've had no luck so far trying to disable them, so...
- Certain stages still have clashing spots on the skybox. Probably most notable in Rallypoint Delta
- The water in Rallypoint Delta does not change color with the stage. I have no idea how to fix this

### Shoutout to HIFU for helping me get lighting effects working and providing some templates!

## CHANGELOG

### 0.1.1
- FASJDKAKSGLK I forgot to add menu config
- Added randomization of unused bridge in default Titanic Plains (located behind the hill between the two cliffs), config separate from Vanilla Plains changes
- Look for some other unused stuff in the future once I can figure out why they're refusing to exist
- Wrote config incorrectly in 0.1.0. Hopefully you won't need to make a new file for it, though
- Cleaned up description a bit

### 0.1.0
- A Lot Happened
- Temporarily removed Artifact of Seasons and all associated components. In its current state, it's just too much of a spaghetti code mess for me to want to try fixing at the moment, and there's other mods out there already if you want to mess around with enemies.
- Rewrote directory to not have like 99% of this mod's content in just two files, hopefully should be easier to sort through the GitHub now
- Implemented do-while loop to force a different choice when a stage is reloaded
- Config added to disable variants as well as the original stage appearance (if all options are deselected for whatever reason, the vanilla stage is used)
- Every existing variant was heavily altered or replaced entirely, adding in lighting effects and focusing on making them less glaring
- Added a second set of variants to Abyssal Depths, Siren's Call, and Scorched Acres. My original reasoning was that there were already three Stage 4s, but with recent mods that randomize or lock stage order, it's not a good excuse anymore
- Added config to make changes to the lighting of vanilla stages (enabled by default)
- Added unused stage props and/or altered existing props to fit certain variants
- Rain effect now shows up in some variants. This WILL break if you never load into the title screen, so config is provided to disable it if you're doing that for whatever reason

### 0.0.6
- Removing networking attempt, as it A. doesn't work and B. seems to be affecting variant rolls
- Added config for BossConfig compatibility
- Couldn't update GitHub with this, will try to fix later

### 0.0.5
- Recolored variants in Roost, Aqueduct, and Depths
- Most darker variants have had their starting fog heavily reduced, should make it easier to see up close
- Red Sundered Grove now only shows up after Stage 4 if using Artifact of Seasons
- Added Commencement variant to config, enabled by default
- Artifact of Seasons icon updated slightly
- Added config option to remove stage-based restrictions when using Artifact of Seasons
- Stage variants should hopefully be consistent between players, haven't had an opportunity to test this though
- Removed random log of Alloy Vulture's spawn card name

### 0.0.4
- Disabled Shrine of the Woods being added to Sundered Grove; it was causing the stage to not spawn interactables at all

### 0.0.3
- Dammit I forgot to add the new dll file
- Updated internal number

### 0.0.2
- Reduced the mid/end colors' transparency of some RampFog effects for clarity
- Upped the brightness of Night!Sky Meadow
- Removed unneeded console logs
- Cleaned up code in a couple other places
- Did get confirmation that OriginalSoundtrack DOES actually work so pog!
- Updated README

### 0.0.1
- First release
