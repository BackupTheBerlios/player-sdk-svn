REFERENCES =				\
	-pkg:gtk-sharp				\
	-r:../src/Player.Sdk.dll

all: db-test.exe kitmanager-test.exe

db-test.exe: DbTest.cs
	mcs DbTest.cs -o $@ $(REFERENCES)

kitmanager-test.exe: KitManagerTest.cs
	mcs KitManagerTest.cs -o $@ $(REFERENCES)
	
