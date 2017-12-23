#Most of the stuff we did in R was typing in specific commands in R Studio, not actually writing functions.  Here is our work
#11.42
#a.
ds = read.csv(“~/Downloads/mypcb.csv”)
summary(ds[9])
summary(ds[6])
summary(ds[8])
summary(ds[2])
summary(ds[4])
hist(ds[[9]])
boxplot(ds[[9]])
hist(ds[[6]],main="Histogram of PCB52")
boxplot(ds[[6]],main="Boxplot of PCB52")
hist(ds[[8]], main="Histogram of PCB118")
boxplot(ds[[8]],main="Boxplot of PCB118")
hist(ds[[2]], main="Histogram of PCB138")
boxplot(ds[[2]],main="Boxplot of PCB138")
hist(ds[[4]], main="Histogram of PCB180")
boxplot(ds[[4]],main="Boxplot of PCB180")

#B.
pcb138 = ds[[2]]
pcb153 = ds[[3]]
pcb180 = ds[[4]]
pcb28 = ds[[5]]
pcb52 = ds[[6]]
pcb126 = ds[[7]]
pcb118 = ds[[8]]
pcb = ds[[9]]
teq = ds[[10]]
teqpcb = ds[[11]]
teqdioxin = ds[[12]]
teqfuran = ds[[13]]



plot(pcb, pcb52)
plot(pcb, pcb118)
plot(pcb, pcb138)
plot(pcb, pcb180)
plot(pcb52, pcb118)
plot(pcb52, pcb138)
plot(pcb52, pcb180)
plot(pcb118, pcb138)
plot(pcb118, pcb180)
plot(pcb138, pcb180)

cor(pcb, pcb52)
cor(pcb, pcb118)
cor(pcb, pcb138)
cor(pcb, pcb180)
cor(pcb52, pcb118)
cor(pcb52, pcb138)
cor(pcb52, pcb180)
cor(pcb118, pcb138)
cor(pcb118, pcb180)
cor(pcb138, pcb180)

#11.43
lm1 = lm(pcb ~ pcb52 + pcb118 + pcb138 + pcb180, data = ds)
coef(lm1)
summary(lm1)
residuals(lm1)
plot(pcb52, residuals(lm1))
plot(pcb118, residuals(lm1))
plot(pcb138, residuals(lm1))
plot(pcb180, residuals(lm1))

#11.44
#For this we ran the same code as before but on a data set loaded from a csv that had the 2 rows removed

#11.45
lm2 = lm(pcb ~ pcb52 + pcb118 + pcb138, data = ds)
coef(lm2)
summary(lm2)
summary(lm1)

#11.46
teq = ds[[10]]
teqpcb = ds[[11]]
teqdioxin = ds[[12]]
teqfuran = ds[[13]]
lm3 = lm(teq ~ teqpcb + teqdioxin + teqfuran, data=ds)
summary(lm3)
coef(lm3)
favstats(lm3)
residuals(lm3)
favstats(residuals(lm3))
plot(lm3,which=1)

#11.47
lm3 = lm(teq ~ pcb52 + pcb118 + pcb138 + pcb180, data = ds)
summary(lm3)
residuals(lm3)
hist(residuals(lm3))
plot(residuals(lm3),pcb52)
plot(pcb52,residuals(lm3))
plot(pcb118,residuals(lm3))
plot(pcb138,residuals(lm3))
plot(pcb180,residuals(lm3))



#11.48 c
#load in data from csv
ds = read.csv("~/Downloads/pcb.csv")
pcb138 = ds[[2]]
pcb153 = ds[[3]]
pcb180 = ds[[4]]
pcb28 = ds[[5]]
pcb52 = ds[[6]]
pcb126 = ds[[7]]
pcb118 = ds[[8]]
pcb = ds[[9]]
teq = ds[[10]]
teqpcb = ds[[11]]
teqdioxin = ds[[12]]
teqfuran = ds[[13]]
df = data.frame(pcb138,pcb153,pcb180,pcb28,pcb52,pcb126,pcb118,pcb,teq,teqpcb,teqdioxin,teqfuran)

repl_zs <- function(col){
  #replaces zeros in a column with half of the minimum positive value
  min_val=min(col[col>0])
  col[col==0] <- (min_val/2)
  col
}

df2 = data.frame(pcb)

for(i in names(df)){
  col_name=paste("log-replaced", i)
  col=log(repl_zs(df[[i]]))
  df2[[i]] <- col
  print(paste("Summary of", col_name))
  print(summary(col))
  boxplot(col, main=paste("Boxplot of", col_name))
}


#11.49
#a)
#iterate over indices of “PCB” columns
for(i1 in c(1,2,3,4,5,6,7)){
  #combine with others for comparison
  for(i2 in (i1+1):8){
    plot(df2[,c(i1, i2)])
    print(cor(df2[,c(i1, i2)]))
    print("")
  }
}

#11.50
#load in data from csv

ds = read.csv("~/Downloads/mypcb.csv")
pcb138 = ds[[2]]
pcb153 = ds[[3]]
pcb180 = ds[[4]]
pcb28 = ds[[5]]
pcb52 = ds[[6]]
pcb126 = ds[[7]]
pcb118 = ds[[8]]
pcb = ds[[9]]
teq = ds[[10]]
teqpcb = ds[[11]]
teqdioxin = ds[[12]]
teqfuran = ds[[13]]
df = data.frame(pcb138,pcb153,pcb180,pcb28,pcb52,pcb126,pcb118,pcb,teq,teqpcb,teqdioxin,teqfuran)

repl_zs <- function(col){
  #replaces zeros in a column with half of the minimum positive value
  min_val=min(col[col>0])
  col[col==0] <- (min_val/2)
  col
}
df2 = data.frame(pcb)

for(i in names(df)){
  col_name=pasteqpcb = ds[[11]]
te("log-replaced", i)
  col=log(repl_zs(df[[i]]))
  df2[[i]] <- col
  print(paste("Summary of", col_name))
  print(summary(col))
  boxplot(col, main=paste("Boxplot of", col_name))
}


lmlogpcb = lm(df2[[1]] ~ df2[[2]] + df2[[3]] + df2[[4]] + df2[[5]] + df2[[6]] + df2[[7]] = df2[[8]], data=df2)
summary(lmlogpcb)
coef(lmlogpcb)



#11.51
# columns have original names of data set but store log of those values (or adjusted value
# instead of log(0) )
lm51 = lm(teq ~ pcb52 + pcb118 + pcb138 + pcb180 + pcb153 + pcb126 + pcb28, data = df2)
summary(lm51)
