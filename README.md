# ModernBusinessWpfSample (.NET 8 / Pure WPF)

WPF 標準機能だけで作った、ライトテーマ標準の業務アプリ風サンプルです。WPF-UI などの外部 UI フレームワークに依存せず、顧客管理・受注管理・レポート出力を含めています。

## 構成

```text
src/
  ModernBusinessWpfSample.Desktop        WPF / View / ViewModel / DI起動
  ModernBusinessWpfSample.Application    UseCase / DTO / interface
  ModernBusinessWpfSample.Domain         Entity / Domain enum / Clock interface
  ModernBusinessWpfSample.Infrastructure JSON保存 / API Client / CSV Exporter
```

## 入っている機能

- ダッシュボード
- 顧客管理
- 受注管理
- 受注CSVレポート出力
- 設定/アーキテクチャメモ
- Generic Host
- DI
- CommunityToolkit.Mvvm
- typed HttpClient
- Microsoft.Extensions.Http.Resilience
- Pure WPF custom chrome
- ライトテーマ標準
- ローカル JSON リポジトリ

## 実行方法

1. Visual Studio 2022 で `ModernBusinessWpfSample.sln` を開く
2. スタートアッププロジェクトを `ModernBusinessWpfSample.Desktop` にする
3. F5で実行

## データ保存先

```text
%LOCALAPPDATA%\ModernBusinessWpfSample
```

顧客と受注は JSON として保存されます。

## レポート出力先

```text
Desktop\ModernBusinessWpfSampleReports
```

## 注意

この生成環境では .NET SDK によるビルド検証はできていません。コードは .NET 8 / Visual Studio 2022 前提で作成しています。

## UI refresh

ライトテーマ前提で、業務アプリらしく以下を調整しています。

- 左ナビゲーションをカード風に変更
- Dashboard / Customers / Orders / Reports / Settings の各画面をカードレイアウト化
- Button / TextBox / DataGrid / ListBoxItem のカスタムスタイル追加
- DockPanel のボタンが横いっぱいに伸びる問題を修正
- ステータスバーを下部に固定


## UI update

WPF-UI の `FluentWindow` を廃止し、標準 WPF の `Window` + `WindowChrome` によるカスタムクロームへ移行しました。グラデーションヘッダー、カードUI、カスタム入力/ボタン/DataGridスタイル、最小化・最大化・閉じるボタンを WPF 標準のみで実装しています。
