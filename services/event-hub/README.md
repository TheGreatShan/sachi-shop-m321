## Please note:

**To successfully produce messages onto Kafka, make sure to enable CGO_ENABLED flag to 1, as the kafka packaged used in this project is written in C**

Powershell:
```powershell
$env:CGO_ENABLED=1
```

**Also make sure you have the C Compiler installed - to verify, type:**

```powershell
gcc --version
```

