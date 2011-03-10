# GNU Make project makefile autogenerated by Premake
ifndef config
  config=debug
endif

ifndef verbose
  SILENT = @
endif

ifndef CC
  CC = gcc
endif

ifndef CXX
  CXX = g++
endif

ifndef AR
  AR = ar
endif

ifeq ($(config),debug)
  OBJDIR     = bin/obj/Debug
  TARGETDIR  = bin/debug
  TARGET     = $(TARGETDIR)/CloudForeverWriter.exe
  DEFINES   += -DWIN -DDEBUG
  INCLUDES  += -I../../../../../../../Documents/Visual\ Studio\ 2008/Projects/LibsAndHeaders/include -IC:/Program\ Files\ \(x86\)/OpenAL\ 1.1\ SDK/include -IC:/OpenCV2.1/include -I../../../../../../../Downloads/msinttypes-r26
  CPPFLAGS  += -MMD -MP $(DEFINES) $(INCLUDES)
  CFLAGS    += $(CPPFLAGS) $(ARCH) -g
  CXXFLAGS  += $(CFLAGS) 
  LDFLAGS   += -L../../../../../../../Documents/Visual\ Studio\ 2008/Projects/LibsAndHeaders/lib
  LIBS      += -lavcodec -lavformat -lavutil -lswscale -lcv210 -lcxcore210 -lhighgui210 -lopenal32
  RESFLAGS  += $(DEFINES) $(INCLUDES) 
  LDDEPS    += 
  LINKCMD    = $(CXX) -o $(TARGET) $(OBJECTS) $(LDFLAGS) $(RESOURCES) $(ARCH) $(LIBS)
  define PREBUILDCMDS
  endef
  define PRELINKCMDS
  endef
  define POSTBUILDCMDS
  endef
endif

ifeq ($(config),release)
  OBJDIR     = bin/obj/Release
  TARGETDIR  = bin/release
  TARGET     = $(TARGETDIR)/CloudForeverWriter.exe
  DEFINES   += -DWIN -DNDEBUG
  INCLUDES  += -I../../../../../../../Documents/Visual\ Studio\ 2008/Projects/LibsAndHeaders/include -IC:/Program\ Files\ \(x86\)/OpenAL\ 1.1\ SDK/include -IC:/OpenCV2.1/include -I../../../../../../../Downloads/msinttypes-r26
  CPPFLAGS  += -MMD -MP $(DEFINES) $(INCLUDES)
  CFLAGS    += $(CPPFLAGS) $(ARCH) -O2
  CXXFLAGS  += $(CFLAGS) 
  LDFLAGS   += -s -L../../../../../../../Documents/Visual\ Studio\ 2008/Projects/LibsAndHeaders/lib
  LIBS      += -lavcodec -lavformat -lavutil -lswscale -lcv210 -lcxcore210 -lhighgui210 -lopenal32
  RESFLAGS  += $(DEFINES) $(INCLUDES) 
  LDDEPS    += 
  LINKCMD    = $(CXX) -o $(TARGET) $(OBJECTS) $(LDFLAGS) $(RESOURCES) $(ARCH) $(LIBS)
  define PREBUILDCMDS
  endef
  define PRELINKCMDS
  endef
  define POSTBUILDCMDS
  endef
endif

ifeq ($(config),debug32)
  OBJDIR     = bin/obj/x32/Debug
  TARGETDIR  = bin/debug
  TARGET     = $(TARGETDIR)/CloudForeverWriter.exe
  DEFINES   += -DWIN -DDEBUG
  INCLUDES  += -I../../../../../../../Documents/Visual\ Studio\ 2008/Projects/LibsAndHeaders/include -IC:/Program\ Files\ \(x86\)/OpenAL\ 1.1\ SDK/include -IC:/OpenCV2.1/include -I../../../../../../../Downloads/msinttypes-r26
  CPPFLAGS  += -MMD -MP $(DEFINES) $(INCLUDES)
  CFLAGS    += $(CPPFLAGS) $(ARCH) -g -m32
  CXXFLAGS  += $(CFLAGS) 
  LDFLAGS   += -m32 -L/usr/lib32 -L../../../../../../../Documents/Visual\ Studio\ 2008/Projects/LibsAndHeaders/lib
  LIBS      += -lavcodec -lavformat -lavutil -lswscale -lcv210 -lcxcore210 -lhighgui210 -lopenal32
  RESFLAGS  += $(DEFINES) $(INCLUDES) 
  LDDEPS    += 
  LINKCMD    = $(CXX) -o $(TARGET) $(OBJECTS) $(LDFLAGS) $(RESOURCES) $(ARCH) $(LIBS)
  define PREBUILDCMDS
  endef
  define PRELINKCMDS
  endef
  define POSTBUILDCMDS
  endef
endif

ifeq ($(config),release32)
  OBJDIR     = bin/obj/x32/Release
  TARGETDIR  = bin/release
  TARGET     = $(TARGETDIR)/CloudForeverWriter.exe
  DEFINES   += -DWIN -DNDEBUG
  INCLUDES  += -I../../../../../../../Documents/Visual\ Studio\ 2008/Projects/LibsAndHeaders/include -IC:/Program\ Files\ \(x86\)/OpenAL\ 1.1\ SDK/include -IC:/OpenCV2.1/include -I../../../../../../../Downloads/msinttypes-r26
  CPPFLAGS  += -MMD -MP $(DEFINES) $(INCLUDES)
  CFLAGS    += $(CPPFLAGS) $(ARCH) -O2 -m32
  CXXFLAGS  += $(CFLAGS) 
  LDFLAGS   += -s -m32 -L/usr/lib32 -L../../../../../../../Documents/Visual\ Studio\ 2008/Projects/LibsAndHeaders/lib
  LIBS      += -lavcodec -lavformat -lavutil -lswscale -lcv210 -lcxcore210 -lhighgui210 -lopenal32
  RESFLAGS  += $(DEFINES) $(INCLUDES) 
  LDDEPS    += 
  LINKCMD    = $(CXX) -o $(TARGET) $(OBJECTS) $(LDFLAGS) $(RESOURCES) $(ARCH) $(LIBS)
  define PREBUILDCMDS
  endef
  define PRELINKCMDS
  endef
  define POSTBUILDCMDS
  endef
endif

OBJECTS := \
	$(OBJDIR)/CloudForeverWriter.o \
	$(OBJDIR)/CloudObserverVirtualWriter.o \
	$(OBJDIR)/list.o \
	$(OBJDIR)/LSD.o \
	$(OBJDIR)/VideoEncoder.o \

RESOURCES := \

SHELLTYPE := msdos
ifeq (,$(ComSpec)$(COMSPEC))
  SHELLTYPE := posix
endif
ifeq (/bin,$(findstring /bin,$(SHELL)))
  SHELLTYPE := posix
endif

.PHONY: clean prebuild prelink

all: $(TARGETDIR) $(OBJDIR) prebuild prelink $(TARGET)
	@:

$(TARGET): $(GCH) $(OBJECTS) $(LDDEPS) $(RESOURCES)
	@echo Linking CloudForeverWriter
	$(SILENT) $(LINKCMD)
	$(POSTBUILDCMDS)

$(TARGETDIR):
	@echo Creating $(TARGETDIR)
ifeq (posix,$(SHELLTYPE))
	$(SILENT) mkdir -p $(TARGETDIR)
else
	$(SILENT) mkdir $(subst /,\\,$(TARGETDIR))
endif

$(OBJDIR):
	@echo Creating $(OBJDIR)
ifeq (posix,$(SHELLTYPE))
	$(SILENT) mkdir -p $(OBJDIR)
else
	$(SILENT) mkdir $(subst /,\\,$(OBJDIR))
endif

clean:
	@echo Cleaning CloudForeverWriter
ifeq (posix,$(SHELLTYPE))
	$(SILENT) rm -f  $(TARGET)
	$(SILENT) rm -rf $(OBJDIR)
else
	$(SILENT) if exist $(subst /,\\,$(TARGET)) del $(subst /,\\,$(TARGET))
	$(SILENT) if exist $(subst /,\\,$(OBJDIR)) rmdir /s /q $(subst /,\\,$(OBJDIR))
endif

prebuild:
	$(PREBUILDCMDS)

prelink:
	$(PRELINKCMDS)

ifneq (,$(PCH))
$(GCH): $(PCH)
	@echo $(notdir $<)
	-$(SILENT) cp $< $(OBJDIR)
	$(SILENT) $(CXX) $(CXXFLAGS) -o "$@" -c "$<"
endif

$(OBJDIR)/CloudForeverWriter.o: ../../Code/CloudForeverWriter.cpp
	@echo $(notdir $<)
	$(SILENT) $(CXX) $(CXXFLAGS) -o "$@" -c "$<"
$(OBJDIR)/CloudObserverVirtualWriter.o: ../../Code/CloudObserverVirtualWriter.cpp
	@echo $(notdir $<)
	$(SILENT) $(CXX) $(CXXFLAGS) -o "$@" -c "$<"
$(OBJDIR)/list.o: ../../Code/list.cpp
	@echo $(notdir $<)
	$(SILENT) $(CXX) $(CXXFLAGS) -o "$@" -c "$<"
$(OBJDIR)/LSD.o: ../../Code/LSD.cpp
	@echo $(notdir $<)
	$(SILENT) $(CXX) $(CXXFLAGS) -o "$@" -c "$<"
$(OBJDIR)/VideoEncoder.o: ../../Code/VideoEncoder.cpp
	@echo $(notdir $<)
	$(SILENT) $(CXX) $(CXXFLAGS) -o "$@" -c "$<"

-include $(OBJECTS:%.o=%.d)
