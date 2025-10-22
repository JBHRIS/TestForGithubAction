# TestForGithubAction.Tests

這是 TestForGithubAction 專案的測試專案，使用 xUnit 測試框架。

## 測試類別

### WeatherForecastTests
單元測試，測試 `WeatherForecast` 記錄類型的功能：
- ✅ 溫度轉換（攝氏轉華氏）
- ✅ 各種溫度值的轉換正確性
- ✅ 初始化與屬性設定
- ✅ 空值摘要處理
- ✅ 負溫度轉換

### WeatherForecastApiTests
整合測試，測試 `/weatherforecast` API 端點：
- ✅ HTTP 狀態碼回應
- ✅ JSON 內容類型
- ✅ 回傳五筆預報資料
- ✅ 資料有效性驗證
- ✅ 日期在未來
- ✅ 摘要值有效性
- ✅ 溫度計算正確性
- ✅ 隨機資料產生

## 執行測試

```bash
dotnet test
```

## 測試涵蓋範圍

- **單元測試**: 5 個測試案例
- **整合測試**: 8 個測試案例
- **總計**: 13 個測試案例
