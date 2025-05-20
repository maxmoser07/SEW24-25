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
db.emps.aggregate([
  { $match: { dept_id: 30 } },
  {
    $project: {
      SAL: 1,
      COMM: { $ifNull: ["$COMM", 0] }
    }
  },
  {
    $group: {
      _id: null,
      avgSAL: { $avg: "$SAL" },
      sumSAL: { $sum: "$SAL" },
      avgCOMM: { $avg: "$COMM" },
      sumCOMM: { $sum: "$COMM" }
    }
  }
]);
// 23. Beispiel
db.emps.aggregate([
  {
    $match: {
      dept_id: 30,
      JOB: { $nin: ["MANAGER", "PRESIDENT"] }
    }
  },
  {
    $group: { _id: "$JOB" }
  },
  {
    $count: "Anzahl_Berufe"
  }
]);
// 24. Beispiel
db.emps.aggregate([
  {
    $group: {
      _id: "$dept_id",
      count: { $sum: 1 }
    }
  },
  {
    $group: {
      _id: null,
      avgMitarbeiter: { $avg: "$count" }
    }
  }
]);
// 25. Beispiel
db.emps.find({
  JOB: { $in: ["MANAGER", "PRESIDENT"] }
});
// 26. Beispiel
db.emps.find({
  COMM: { $exists: true, $ne: null },
  $expr: { $gt: ["$COMM", { $divide: ["$SAL", 4] }] }
}, {
  ENAME: 1,
  JOB: 1,
  COMM: 1
});
// 27. Beispiel
db.emps.aggregate([
  {
    $project: {
      ENAME: 1,
      total: { $add: ["$SAL", { $ifNull: ["$COMM", 0] }] }
    }
  },
  { $sort: { total: 1 } },
  { $limit: 1 }
]);
// 28. Beispiel
db.emps.find().sort({ HIREDATE: 1 }).limit(1);
// 29. Beispiel
db.emps.aggregate([
  {
    $group: {
      _id: { dept_id: "$dept_id", JOB: "$JOB" },
      count: { $sum: 1 }
    }
  }
]);
// 30. Beispiel
db.emps.aggregate([
  {
    $project: {
      dept_id: 1,
      income: { $add: ["$SAL", { $ifNull: ["$COMM", 0] }] }
    }
  },
  {
    $group: {
      _id: "$dept_id",
      totalIncome: { $sum: "$income" }
    }
  },
  { $sort: { totalIncome: -1 } }
]);
// 34. Beispiel
db.emps.aggregate([
  { $match: { dept_id: 30 } },
  {
    $project: {
      SAL: 1,
      COMM: { $ifNull: ["$COMM", 0] }
    }
  },
  {
    $group: {
      _id: null,
      minSAL: { $min: "$SAL" },
      maxSAL: { $max: "$SAL" },
      avgSAL: { $avg: "$SAL" },
      countSAL: { $sum: 1 },
      minCOMM: { $min: "$COMM" },
      maxCOMM: { $max: "$COMM" },
      avgCOMM: { $avg: "$COMM" },
      countCOMM: { $sum: 1 }
    }
  }
]);
// 35. Beispiel
db.emps.aggregate([
  {
    $group: {
      _id: "$dept_id",
      minSAL: { $min: "$SAL" },
      maxSAL: { $max: "$SAL" },
      avgSAL: { $avg: "$SAL" }
    }
  }
]);