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

// 8. Beispiel
db.emps.find({
  JOB: { $nin: ["MANAGER", "PRESIDENT"] }
}, {
  ENAME: 1, JOB: 1, _id: 0
});
// 9. Beispiel
db.emps.find({
  ENAME: { $regex: /^.{2}A/i }
}, {
  ENAME: 1, _id: 0
});
// 10. Beispiel
db.emps.find({
  COMM: { $ne: null }
}, {
  id: 1, ENAME: 1, JOB: 1, COMM: 1, _id: 0
});
// 11. Beispiel
db.emps.find({}, {
  ENAME: 1, COMM: 1, _id: 0
}).sort({ COMM: 1 });
// 12. Beispiel
db.emps.find({
  JOB: { $nin: ["MANAGER", "PRESIDENT"] }
}).sort({ dept_id: 1, HIREDATE: -1 });
// 13. Beispiel
db.emps.find({
  ENAME: { $regex: /^.{6}$/ }
}, {
  ENAME: 1, _id: 0
});
// 14. Beispiel
db.emps.aggregate([
  { $match: { dept_id: 30 } },
  {
    $project: {
      _id: 0,
      "Mitarbeiter und Tätigkeit": {
        $concat: ["$ENAME", " - ", "$JOB"]
      }
    }
  }
]);
// 16. Beispiel
db.emps.aggregate([
  {
    $project: {
      _id: 0,
      ENAME: 1,
      MONTHLY: "$SAL",
      DAILY: { $round: [{ $divide: ["$SAL", 22] }, 2] },
      HOURLY: { $round: [{ $divide: ["$SAL", 176] }, 2] }
    }
  }
]);
// 17. Beispiel
db.emps.aggregate([
  {
    $group: {
      _id: null,
      totalMonthlySalary: { $sum: "$SAL" }
    }
  },
  {
    $project: {
      _id: 0,
      totalMonthlySalary: 1
    }
  }
]);
// 19. Beispiel
db.emps.aggregate([
  { $match: { dept_id: 30 } },
  {
    $group: {
      _id: null,
      mitGehalt: { $sum: { $cond: [{ $gt: ["$SAL", 0] }, 1, 0] } },
      mitProvision: { $sum: { $cond: [{ $ifNull: ["$COMM", false] }, 1, 0] } }
    }
  },
  {
    $project: {
      _id: 0,
      mitGehalt: 1,
      mitProvision: 1
    }
  }
]);
// 20. Beispiel

// 21. Beispiel
db.emps.aggregate([
  {
    $group: {
      _id: "parent_id"
    } }
]);
// 22. Beispiel
