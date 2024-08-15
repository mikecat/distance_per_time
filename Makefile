TARGET=DistancePerTime.exe
OPTIONS= \
	/target:winexe \
	/optimize+ \
	/warn:4 \
	/codepage:65001 \
	/reference:TrainCrewInput.dll

SOURCES= \
	AssemblyInfo.cs \
	DistancePerTime.cs

$(TARGET): $(SOURCES)
	csc /out:$@ $(OPTIONS) $^
