Tdata =importdata("Tdata.txt");
Tr = Tdata(:,1);
Ti = Tdata(:,2);
T = Tr+1i.*Ti;
plot(abs(T));
kaiser1 = importdata("kaisar.txt");
figure
plot(kaiser1)
hold on 
win1 = kaiser(2048,6);
plot(win1)