# API Response Examples

## GET /api/dashboards/initoee

```json
{
  "machines": [
    { "machineId": 1, "machineName": "PP12/A" }
  ],
  "lastRecords": [
    {
      "machineId": 1,
      "recordDateString": "2024-03-26",
      "availability": 100.00,
      "performance": 95.50,
      // ...other fields...
    }
  ]
}
```

## GET /api/dashboards/latest-records

```json
[
  {
    "machineId": 1,
    "machineName": "PP12/A",
    "recordDate": "2024-03-26T00:00:00",
    // ...other fields...
  }
]
```
