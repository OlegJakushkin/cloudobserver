project "cf-util"
	kind "StaticLib"
	language "C++"
	location ( "projects/" .. os.get() .. "-" .. action )
	files { "src/cf-util/**.h", "src/cf-util/**.cpp" }
	
	cloud.project.init()
	cloud.project.useBoost()
	
	configuration "gmake"
		buildoptions { "-fPIC" }
	
	configuration "Debug"
		defines { "DEBUG" }
		flags { "Symbols", "Unicode" }
	
	configuration "Release"
		defines { "NDEBUG" }
		flags { "OptimizeSpeed", "Unicode" }