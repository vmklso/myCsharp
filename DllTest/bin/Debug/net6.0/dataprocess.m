path = "./20220720/1.s2p";
opts = detectImportOptions(path);
opts.Dataline=[9,-1];
data = readtable(path,opts);