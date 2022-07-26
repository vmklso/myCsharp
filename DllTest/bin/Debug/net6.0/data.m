close all;
clear;
clc;

figure
F = importdata("WFdata.txt");
F = F(:,1)+1i*F(:,2);
plot(abs(F))

figure
A = importdata("WAdata.txt");
A = A(:,1);
plot(A)
figure

B = importdata("WAdata1.txt");
B = B(:,1);
plot(B)