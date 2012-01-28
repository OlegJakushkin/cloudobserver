cloud.project = {}

function cloud.project.init()
	--
	cloud.addLibDir(  _OPTIONS["libsPath"] )
	cloud.addIncludeDir( _OPTIONS["includesPath"] )
	libdirs {
		"./"
		}
	if os.get() == "windows" then
		defines { "WIN" }
	end
	
	if os.get() == "linux" then
		defines { "LIN" }
	end
	
	if os.get() == "macosx" then
		defines { "MAC" }
		links {
		"Carbon.framework"
		}
		
		libdirs {
		"/usr/lib",
		"/usr/local/lib"
		}
		
		includedirs {
		"/usr/include",
		"/usr/local/include"
		}
	end 
end

function cloud.project.useCV()
	--
	cloud.addLibDir( _OPTIONS["OpenCVLibsPath"] )
	cloud.addIncludeDir(  _OPTIONS["OpenCVIncludesPath"] )
	if os.get() == "windows" then
		defines { "WIN" }
		if  _OPTIONS["OpenCVshared"] then
				links {
					"opencv_core231",
					"opencv_highgui231",
					"opencv_imgproc231"
				}
			
				cloud.win.copyDLL("OpenCV", "opencv_core231.dll")
				cloud.win.copyDLL("OpenCV", "opencv_highgui231.dll")
				cloud.win.copyDLL("OpenCV", "opencv_imgproc231.dll")
			else	
				configuration { }
				
				configuration { "Debug" }
					links {
						"opencv_core231d",
						"opencv_highgui231d",
						"opencv_imgproc231d",
						"libjasperd",
						"libjpegd",
						"libpngd",
						"libtiffd",
						"zlibd",
						"vfw32",
						"comctl32"
					}
				configuration { }
				
				configuration { "Release" }
					links {
						"opencv_core231",
						"opencv_highgui231",
						"opencv_imgproc231",
						"libjasper",
						"libjpeg",
						"libpng",
						"libtiff",
						"zlib",
						"vfw32",
						"comctl32"
					}
				configuration { }
			end
		cloud.win.addLibFromProgrammFiles("OpenCV")
		cloud.win.addLibDirFromProgrammFiles("OpenCV/share/OpenCV/3rdparty/lib")
	end
	
	if os.get() == "linux" then
		defines { "LIN" }
		links {
			":libopencv_imgproc.a",
			":libopencv_core.a",
			":libopencv_highgui.a"
		}
		links {
			":liblibjasper.a",
			":liblibjpeg.a",
			":liblibpng.a",
			":liblibtiff.a",
			":libzlib.a"
		}
	end
	
	if os.get() == "macosx" then
		defines { "MAC" }
		links {
			":libopencv_imgproc.a",
			":libopencv_core.a",
			":libopencv_highgui.a",
			"QuickTime.framework"
		}
		
		links {
			":liblibjasper.a",
			":liblibjpeg.a",
			":liblibpng.a",
			":liblibtiff.a",
			":libzlib.a",
			"AppKit.framework",
			"CoreVideo.framework",
			"QTKit.framework",
		}
	end 
end



function cloud.project.useBoost()
	--
	cloud.addIncludeDir( _OPTIONS["BoostIncludesPath"] )
	cloud.addLibDir( _OPTIONS["BoostLibsPath"])
		includedirs {"3rdparty/header-only"}
		defines{ "BOOST_ASIO_DISABLE_IOCP" }
	if os.get() == "windows" then
		defines { "WIN" } 
		cloud.win.addIncludeDirFromProgrammFiles("Boost/include/boost-1_48")
		cloud.win.addLibDirFromProgrammFiles("Boost/lib")
	end	
	if os.get() == "linux" then
		defines { "LIN" }
		links {
			":libboost_regex.a",
			":libboost_system.a",
			":libboost_thread.a",
			":libboost_date_time.a",
			":libboost_filesystem.a",
			":libboost_program_options.a",
			":libboost_iostreams.a",
			":libboost_serialization.a",
			":libboost_zlib.a"
		}
		
		links {
			"dl",
			"pthread"
		}
	end
	if os.get() == "macosx" then
		defines { "MAC" }
		links {
			":libboost_regex.a",
			":libboost_system.a",
			":libboost_thread.a",
			":libboost_filesystem.a",
			":libboost_program_options.a",
			":libboost_date_time.a",
			":libboost_iostreams.a",
			":libboost_serialization.a",
			":libboost_zlib.a"
		}
		
		links {
			"dl",
			"pthread"
		}	
	end 
end


function cloud.project.useMPI()
	--
	cloud.project.useBoost()
	defines { "MPI" }
	if os.get() == "windows" then
		links
		{
			"msmpi"
		}
		libdirs 
		{
		"C:/Program Files/Microsoft HPC Pack 2008 SDK/Lib/i386" -- there is also "C:/Program Files/Microsoft HPC Pack 2008 SDK/Lib/amd64"
		}
		
		includedirs 
		{
		"C:/Program Files/Microsoft HPC Pack 2008 SDK/Include"
		}
		
	end	
	if os.get() == "linux" then
		links {
		"boost_mpi",
		"boost_serialization"
		}
	end
	if os.get() == "macosx" then
		links {
		"boost_mpi",
		"boost_serialization"
		}	
	end 
end



function cloud.project.useopenSSL()
	cloud.addLibDir(  _OPTIONS["OpenSSLLibsPath"] )
	cloud.addIncludeDir(  _OPTIONS["OpenSSLIncludesPath"] )
		
	if os.get() == "windows" then
		defines { "WIN" }
		links {
		"libeay32"
		}
		cloud.win.addLibFromProgrammFiles("OpenSSL")
		if  _OPTIONS["CopySharedLibraries"] then
			cloud.win.copyDLL("OpenSSL/lib", "libeay32.dll")
		end
	end
	
	if os.get() == "linux" then
		defines { "LIN" }
		links {
		":libcrypto.a"
		}
	end
	
	if os.get() == "macosx" then
		defines { "MAC" }
		links {
		":libcrypto.a"
		}
	end 
	
end

function cloud.project.copyConfig()
	if not os.isdir("projects/" .. os.get() .. "-" .. action .. "/bin/debug" ) then
		os.mkdir("projects/" .. os.get() .. "-" .. action .. "/bin/debug")
	end
	if not os.isdir("projects/" .. os.get() .. "-" .. action .."/bin/release" ) then
		os.mkdir("projects/" .. os.get() .. "-" .. action .. "/bin/release")
	end
	os.copyfile("assets/config.xml" , "projects/" .. os.get() .. "-" .. action .. "/config.xml" )
	os.copyfile("assets/config.xml" , "projects/" .. os.get() .. "-" .. action .. "/bin/debug/config.xml" )
	os.copyfile("assets/config.xml" , "projects/" .. os.get() .. "-" .. action .. "/bin/release/config.xml" )
end

function cloud.project.createDumpsFolder()
	if not os.isdir("projects/" .. os.get() .. "-" .. action .. "/bin/debug" ) then
		os.mkdir("projects/" .. os.get() .. "-" .. action .. "/bin/debug")
	end
	if not os.isdir("projects/" .. os.get() .. "-" .. action .."/bin/release" ) then
		os.mkdir("projects/" .. os.get() .. "-" .. action .. "/bin/release")
	end
	os.mkdir("projects/" .. os.get() .. "-" .. action .. "/dumps")
	os.mkdir("projects/" .. os.get() .. "-" .. action .. "/bin/debug/dumps")
	os.mkdir("projects/" .. os.get() .. "-" .. action .. "/bin/release/dumps")
end

function cloud.project.copyHtdocsFolder()
	if not os.isdir("projects/" .. os.get() .. "-" .. action .. "/bin/debug/htdocs" ) then
		os.mkdir("projects/" .. os.get() .. "-" .. action .. "/bin/debug/htdocs")
	end
	if not os.isdir("projects/" .. os.get() .. "-" .. action .."/bin/release/htdocs" ) then
		os.mkdir("projects/" .. os.get() .. "-" .. action .. "/bin/release/htdocs")
	end
	os.copydir("assets/htdocs",  "projects/" .. os.get() .. "-" .. action .. "/bin/debug/htdocs")
	os.copydir("assets/htdocs",  "projects/" .. os.get() .. "-" .. action .. "/bin/release/htdocs")
	os.copydir("assets/htdocs",  "projects/" .. os.get() .. "-" .. action .. "/htdocs")
end
