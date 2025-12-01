# .NET 10 性能改進專案

本專案展示 .NET 10.0 引入的各項性能改進，包含基準測試和範例，突顯此版本框架的增強功能。

## 📋 目錄

[TOC]

## 🎯 概述

.NET 10 的性能改進不是單一的重大突破，而是數千個精心優化的累積成果。這些改進在奈秒級和位元組級別上削減開銷，針對執行數萬億次的操作進行優化，最終轉化為實際應用的顯著性能提升。

## 🚀 主要改進領域

### JIT 編譯器優化

#### 1. **去抽象化 (Deabstraction)**

- **物件堆疊分配**: 允許某些物件在堆疊上分配而非堆積，減少 GC 壓力
- **去虛擬化**: 改進對虛擬方法和介面呼叫的優化，特別是針對陣列介面實作的去虛擬化

#### 2. **邊界檢查消除**

- 智能消除不必要的陣列邊界檢查
- 從 `switch` 目標建立斷言，在已知長度的情況下移除邊界檢查
- 針對索引模式（如 `arr[i+0]`, `arr[i+1]`）的優化

#### 3. **程式碼克隆 (Cloning)**

- 為 `Span<T>` 啟用迴圈克隆，產生快速和慢速路徑版本
- 允許克隆 `try/finally` 區塊，改進常見模式的性能

#### 4. **內聯優化**

- 改進包含 `Monitor.Enter/Exit` 的方法內聯
- 減少呼叫開銷，改善熱路徑性能

#### 5. **常數折疊**

- 更智能地識別和折疊常數運算式
- 消除多餘的 null 檢查
- 啟用後續優化如死碼消除

#### 6. **指令集支援**

- **AVX10.2**: 新增支援和內建函式
- **Arm SVE**: 可擴展向量擴展支援
- **APX**: 進階性能擴展
- **GFNI 和 VPCLMULQDQ**: 密碼學和資料處理優化

### 執行時期改進

#### 1. **垃圾收集器 (GC)**

- **DATAS 調整**: 改進動態適應應用程式大小的行為
- 減少不必要的收集次數
- 更平穩的暫停時間
- 更好的記憶體碎片管理

#### 2. **執行緒優化**

- `Task.WhenAll` 避免臨時集合分配
- 單一任務時直接返回，無額外開銷
- 減少工作集大小

#### 3. **新型別化 GC 控制代碼**

- `GCHandle<T>`, `PinnedGCHandle<T>`, `WeakGCHandle<T>`
- 強型別，減少轉換開銷
- 更安全且性能更好

### 集合與 LINQ

#### 1. **LINQ 優化**

- `CountBy` 和 `AggregateBy`: 新的高效聚合方法
- `Index`: 為元素添加索引的優化方法
- 改進的 `ToArray` 和 `ToList` 在 `Skip`/`Take` 之後的性能
- Native AOT 的優化

#### 2. **凍結集合 (Frozen Collections)**

- 對密集列舉值的直接索引查找優化
- 減少雜湊運算開銷
- `FrozenSet<T>` 的 `Overlaps` 和 `SetEquals` 優化

#### 3. **BitArray**

- 位元組建構函式的向量化
- 大幅提升大型陣列的建構速度

### I/O 與網路

#### 1. **壓縮**

- 更新到 `zlib-ng` 2.2.5
- 改進 AVX2 和 AVX512 使用
- 修復高度可壓縮資料的回歸問題
- `GZipStream` 的流重置優化

#### 2. **檔案系統**

- Windows `FileSystemWatcher` 使用原生緩衝區
- 減少記憶體分配和固定開銷

#### 3. **網路**

- `IPAddress.Parse` 支援 UTF-8 位元組直接解析
- `Uri` 建構優化，特別是 IPv6 地址
- HTTP/3 可修剪支援（Native AOT）

### 基元與數值運算

#### 1. **Guid**

- 直接從 UTF-8 位元組解析
- 避免轉碼開銷

#### 2. **BigInteger**

- `TryWriteBytes` 使用直接記憶體複製
- 非負數值的優化路徑

#### 3. **TensorPrimitives**

- 新增 70 多個操作重載
- `Half` 型別的向量化支援
- `Softmax` 和 `LogSoftmax` 的融合優化
- 改進的 SIMD 實作

### JSON

#### 1. **序列化優化**

- `Utf8JsonWriter` 和相關物件的快取改進
- 並行場景的性能提升

#### 2. **新 API**

- `JsonObject.TryAdd` 和 `JsonNode.ReplaceWith`
- `JsonArray.RemoveAll`: 避免 O(N²) 操作
- `WriteBase64BytesAsync`: 串流處理 Base64 編碼

### 搜尋與比對

#### 1. **向量化搜尋**

- `IndexOfAnyInRange` 的更廣泛應用
- `String.Normalize` 使用向量化實作
- `HttpUtility.UrlDecode` 優化

#### 2. **正規表達式**

- 改進的向量化搜尋
- 更好的字元類別處理

### 診斷與加密

#### 1. **Stopwatch**

- `GetTimestamp` 和 `GetElapsedTime` 的彙編優化
- 減少方法呼叫開銷

#### 2. **加密**

- SHA-256 的原生最佳化（Windows、macOS、Linux）
- `SymmetricAlgorithm.SetKey`: 新的基於 span 的方法
- 避免不必要的陣列分配

## 🔧 基準測試設定

所有基準測試使用 BenchmarkDotNet 0.15.2 執行，建議同時安裝 .NET 9 和 .NET 10 以進行比較：

```bash
dotnet new console -o benchmarks
cd benchmarks
```

在 Linux Ubuntu 24.04.1 LTS 上測試：

- 處理器：11th Gen Intel Core i9-11950H 2.60GHz
- 執行時期：.NET 9.0.9 vs .NET 10.0

## 📊 性能提升摘要

| 領域 | 主要改進 | 影響範圍 |
|------|---------|---------|
| JIT 編譯器 | 邊界檢查消除、去虛擬化、內聯 | 所有應用程式 |
| 垃圾收集 | DATAS 調整、更少的收集 | 記憶體密集型應用 |
| LINQ | 新方法、優化現有操作 | 資料處理 |
| JSON | 序列化快取、新 API | Web 服務、API |
| I/O | 壓縮、檔案系統 | 檔案處理、網路 |
| 數值運算 | TensorPrimitives 向量化 | 科學計算、ML |

## 🎓 關鍵要點

1. **累積效應**: .NET 10 包含數千個小型優化，累積產生顯著效果
2. **自動受益**: 大多數改進無需修改程式碼即可獲得
3. **向量化優先**: 廣泛使用 SIMD 指令進行加速
4. **記憶體效率**: 減少分配、改進 GC、更好的快取利用
5. **跨平台**: Windows、Linux、macOS 均有特定優化

## 📚 參考資源

- [官方部落格文章](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-10/)
- [.NET 10 下載](https://dotnet.microsoft.com/download/dotnet/10.0)
- [BenchmarkDotNet](https://www.nuget.org/packages/BenchmarkDotNet/)

## 🏗️ 專案結構

```text
Performance-Improvements-In-NET10/
├── src/                          # 原始碼範例
├── test/                         # 測試專案
├── benchmarks/                   # 基準測試（建議建立）
├── README.md                     # 本檔案
└── NET10PerformanceImprovements.slnx
```

## 🔜 下一步

要完全體驗 .NET 10 的性能改進：

1. 升級到 .NET 10
2. 執行您的應用程式基準測試
3. 探索新 API（如 `CountBy`, `AggregateBy`, `JsonArray.RemoveAll`）
4. 利用向量化操作（`TensorPrimitives`）
5. 為效能關鍵程式碼測試 Native AOT

---

**注意**: 本專案基於 .NET 10 RC1 版本的性能改進文章。正式版本可能包含額外的優化和改進。
