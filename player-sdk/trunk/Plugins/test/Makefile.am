REFERENCES=								\
		/r:../../src/Player.Sdk.dll

all: plugin-test.dll

plugin-test.dll: PluginTest.cs
	mcs -o $@ $(REFERENCES) -t:library PluginTest.cs

pluginsdir=$(prefix)/lib/Player.Sdk/Plugins
plugins_DATA= plugin-test.dll

CLEANFILES=*.dll
EXTRA_DIST=*.cs

