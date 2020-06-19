
% add library path to search path
mfilepath=fileparts(which(mfilename));
%disp(mfilepath);
addpath(fullfile(mfilepath,'./liblsl-Matlab'));
% todo: check the below lines code called many times
if ismac
    % Code to run on Mac platform
    addpath(fullfile(mfilepath,'./bin/mac'));
    addpath(fullfile(mfilepath,'./liblsl-Matlab'));
elseif isunix
    % Code to run on Linux platform
    addpath(fullfile(mfilepath,'./bin/linux'));
elseif ispc
    % Code to run on Windows platform
    addpath(fullfile(mfilepath,'./bin/win64'));
else
    disp('Platform not supported');
end
%% instantiate the library
try
    lib = lsl_loadlib(env_translatepath('dependencies:/liblsl-Matlab/bin'));
catch
    lib = lsl_loadlib();
end

% resolve a stream...
disp('Resolving an EEG stream...');
result = {};
while isempty(result)
    streamName = 'EmotivDataStream-EEG'; % name of stream. the name is one of vale EmotivDataStream-Motion,
                                         % EmotivDataStream-EEG , 'EmotivDataStream-Performance Metrics'
    result = lsl_resolve_byprop(lib,'name', streamName); 
end

% create a new inlet
disp('Opening an inlet...');
inlet = lsl_inlet(result{1});

% get the full stream info (including custom meta-data) and dissect it
inf = inlet.info();
fprintf('The stream''s XML meta-data is: \n');
fprintf([inf.as_xml() '\n']);
fprintf(['The manufacturer is: ' inf.desc().child_value('manufacturer') '\n']);
fprintf('The channel labels are as follows:\n');
ch = inf.desc().child('channels').child('channel');
for k = 1:inf.channel_count()
    fprintf(['  ' ch.child_value('label') '\n']);
    ch = ch.next_sibling();
end

disp('Now receiving data...');
while true
    % get data from the inlet
    [vec,ts] = inlet.pull_sample();
    % and display it
    fprintf('%.2f\t',vec);
    fprintf('%.5f\n',ts);
end