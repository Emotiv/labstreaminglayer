%% instantiate the library
disp('Loading library...');
lib = lsl_loadlib();

% make a new stream outlet
disp('Creating a new streaminfo...');
%info = lsl_streaminfo(lib,'BioSemi','Markers',2,100,'cf_float32','matlab123');

info = lsl_streaminfo(lib,'MetaTester','Markers',2,100,'cf_float32','matlab123');
chns = info.desc().append_child('channels');
for label = {'MarkerValue','TimeStamp'}
    ch = chns.append_child('channel');
    ch.append_child_value('label',label{1});
end
info.desc().append_child_value('manufacturer','Matlab');

disp('Opening an outlet...');
disp(info)
outlet = lsl_outlet(info);

% send data into the outlet, sample by sample
disp('Now transmitting data...');
while true
    r = randi([1,20],1,2);
    disp(r);
    outlet.push_sample(r);
    pause(1);
end