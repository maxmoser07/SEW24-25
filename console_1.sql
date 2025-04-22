// 1. Beispiel
db.depts.find({},{_id:0})

// 2. Beispiel
db.emps.find({dept_id:10},{_id:0,ENAME:1, JOB:1, HIREDATE:1});

// 3. Beispiel
db.emps.find({JOB:"CLERK"},{_id:0,ENAME:1, JOB:1, SAL:1});

// 4. Beispiel
db.emps.find({dept_id:{$ne:10}},{_id:0});

// 4,1. Beispiel
db.emps.find({$and:[{dept_id:{$ne:10}},{dept_id:{$ne:30}}]}, {_id:0});

// 7. Beispiel
db.emps.find({$or:[{SAL:{$lte: 1250}},{SAL:{$gte: 1600}}]},{_id:0});