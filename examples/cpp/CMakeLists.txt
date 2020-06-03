cmake_minimum_required(VERSION 3.5)
project(Examples
    LANGUAGES C CXX
    VERSION 0.2.0)

set(LIB_BIN_DIR ${PROJECT_SOURCE_DIR}/bin)
set(LIB_DIR ${PROJECT_SOURCE_DIR}/lib)
set(INCLUDE_DIR ${PROJECT_SOURCE_DIR}/include)

if (WIN32)
    
    set(PROJECT_LINK_LIBS lsl)
    if(WIN64)
        link_directories(${LIB_DIR}/win64)
        set(LSL_DLLS ${LIB_BIN_DIR}/win64/lsl.dll)
        # set(CMAKE_EXE_LINKER_FLAGS "/machine:x64")
    else()
        link_directories( ${LIB_DIR}/win32)
        set(LSL_DLLS ${LIB_BIN_DIR}/win32/lsl.dll)
    endif()
elseif(APPLE)
    set ( PROJECT_LINK_LIBS "-framework lsl" )
elseif(UNIX)
    set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -fPIC -fvisibility=hidden -Wall -Wextra")
    set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++1y -fPIC -fvisibility=hidden -Wall -Wextra")
    set (PROJECT_LINK_LIBS lsl )
    if(${CMAKE_SYSTEM_PROCESSOR} STREQUAL "x86_64")
       link_directories( ${LIB_BIN_DIR}/linux64 )
    elseif(${CMAKE_SYSTEM_PROCESSOR} STREQUAL "x86")
       link_directories( ${LIB_BIN_DIR}/linux32 )
    else()
       link_directories( ${LIB_BIN_DIR}/armhf )
    endif()
endif()

# include(Findliblsl.cmake)

# convenience function to add an example file
# this creates a target, links the necessary libraries and
# creates an install target
function(addlslexample name extension)
    add_executable(${name}
        ${name}.${extension}
    )
    # target_link_libraries(${name} PRIVATE LSL::lsl)
    target_include_directories(${name} PRIVATE  ${INCLUDE_DIR})
    target_link_libraries(${name} ${PROJECT_LINK_LIBS})
    target_compile_features(${name} PRIVATE cxx_constexpr)
    # installLSLApp(${name})
endfunction()

addlslexample(SendSimpleMarker cpp)
addlslexample(SendMarker cpp)
addlslexample(ReceiveData cpp)

