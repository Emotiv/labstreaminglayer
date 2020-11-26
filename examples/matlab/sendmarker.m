% add library path to search path
mfilepath=fileparts(which(mfilename));
addpath(fullfile(mfilepath,'./liblsl-Matlab'));
%disp(mfilepath);

lib_path = "";
% todo: check the below lines code called many times
if ismac
    % Code to run on Mac platform
    lib_path = fullfile(mfilepath,'./bin/mac');

elseif isunix
    % Code to run on Linux platform
    disp('Unsupported linux yet.')
elseif ispc
    % Code to run on Windows platform
    lib_path = fullfile(mfilepath,'./bin/win64');
else
    disp('Platform not supported')
end

%% instantiate the library
disp('Loading library...');
try
    lib = lsl_loadlib(env_translatepath('dependencies:/liblsl-Matlab/bin'));
catch
    fprintf('Load libarry from path %s \n',lib_path);
    lib = lsl_loadlib(lib_path);
end

% make a new stream outlet
disp('Creating a new streaminfo...');
%info = lsl_streaminfo(lib,'BioSemi','Markers',2,100,'cf_float32','matlab123');

info = lsl_streaminfo(lib,'MatlabMarker','Markers', 3, 100,'cf_double64','matlab1234');
chns = info.desc().append_child('channels');
for label = {'MarkerTime', 'MarkerValue','CurrentTime'}
    ch = chns.append_child('channel');
    ch.append_child_value('label',label{1});
    ch.append_child_value('unit','interge');
    ch.append_child_value('type','Marker');
end
info.desc().append_child_value('manufacturer','Matlab');

disp('Opening an outlet...');
disp(info)
outlet = lsl_outlet(info);
% send data into the outlet, sample by sample
disp('Now send marker data...');
while true
    t = datetime('now','TimeZone','local','Format','d-MMM-y HH:mm:ss Z');
    %disp(t);
    epocTimeNow = posixtime(t); % convert to epoch time
    %sprintf('%16.4f',epocTimeNow);
    markerValue = randi(100);
    data = [epocTimeNow, markerValue, epocTimeNow];
    %disp(data);
    disp('Send marker has value: ');
    disp(markerValue);
    outlet.push_sample(data);
    pause(1);
end
