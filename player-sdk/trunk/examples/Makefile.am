REFERENCES =				\
	-pkg:gtk-sharp				\
	-r:../src/Player.Sdk.dll

all: db-test.exe kitmanager-test.exe player-test.exe pluginmanager-test.exe

db-test.exe: DbTest.cs
	mcs DbTest.cs -o $@ $(REFERENCES)

player-test.exe: PlayerTest.cs
	mcs PlayerTest.cs -o $@ $(REFERENCES)
	
kitmanager-test.exe: KitManagerTest.cs
	mcs KitManagerTest.cs -o $@ $(REFERENCES)

pluginmanager-test.exe: PluginManagerTest.cs
	mcs PluginManagerTest.cs -o $@ $(REFERENCES)
	
EXTRA_DIST= *.cs test.ogg
CLEANFILES= *.exe
