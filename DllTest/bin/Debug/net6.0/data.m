Tdata =importdata("Tdata.txt");
Tr = Tdata(:,1);
Ti = Tdata(:,2);
T = Tr+1i.*Ti;
plot(abs(T));
kaiser1 = importdata("kaisar.txt");
figure
plot(kaiser1)
hold on 
win1 = kaiser(3176,6);
plot(win1)
figure
F = importdata("WFdata.txt");
F = F(:,1);
plot(abs(F))
data2 = importdata("WAdata.txt");
data2 = data2(:,1);
figure
plot(data2);