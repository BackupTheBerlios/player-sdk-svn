ASSEMBLER = monodoc --assemble
UPDATER = monodoc --update

all: assemble

assemble: player-sdk.zip player-sdk.tree

player-sdk.zip player-sdk.tree: en/*/*.xml
	$(ASSEMBLER) --ecma $(srcdir)/en -o player-sdk 

update:
	monodoc --update ../src/Player.Sdk.dll -o ./en

docsdir = `monodoc --get-sourcesdir`
docs_DATA = player-sdk.source player-sdk.tree player-sdk.zip

dist-hook:
	mkdir -p $(distdir)/en
	cp en/*.xml $(distdir)/en/
	for i in $(NAMESPACES); do				\
		mkdir -p $(distdir)/en/$$i;			\
		cp $(srcdir)/en/$$i/*.xml $(distdir)/en/$$i;	\
	done

CLEANFILES = player-sdk.tree player-sdk.zip 

EXTRA_DIST =			\
	player-sdk.source
