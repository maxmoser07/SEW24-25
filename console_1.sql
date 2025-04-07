// 1. Beispiel
db.depts.find({},{_id:0})

// 2. Beispiel
db.emps.find({dept_id:10},{_id:0,ENAME:1, JOB:1, HIREDATE:1});

// 3. Beispiel
db.emps.find({JOB:"CLERK"},{_id:0,ENAME:1, JOB:1, SAL:1});