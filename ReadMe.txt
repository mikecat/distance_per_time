距離÷時間
==========

## これは何？

TRAIN CREW 向けの運転支援ソフトです。

次のポイント (駅などの到達すべきタイミングが決まっている地点のこと。分岐器ではない)
までの距離を同ポイントに着くべきタイミングまでの時間で割り、
走行するべき速度の目安を表示します。
また、今の速度で走行し続けた場合に次のポイントに着くタイミングの予測も表示します。

## English UI

You can use "Distance per Time" in English UI mode by launching it via "English_UI.bat".
English UI mode also can be enabled by adding "--english" option
when launching "DistancePerTime.exe".

## 各項目の説明

### 次のポイントまでの距離は

次のポイントまでの距離を、小数点以下を切り捨ててメートル単位で表示します。

### 次のポイントまでの時間は

次のポイントに着くべきタイミングまでの時間を、小数点以下を切り上げて秒単位で表示します。
「着くべきタイミング」は到着予定時刻なので、
ゲーム画面に表示されているのが出発予定時刻の場合は表示とズレることがあります。

### この距離をこの時間で割ると

次のポイントまでの距離を、次のポイントに着くべきタイミングまでの時間で割った結果を表示します。
結果が 1000 [m/s] 以上の場合は、「∞」と表示します。
また、次のポイントに着くべきタイミングまでの時間が 0 秒以下の場合も、「∞」と表示します。

### すなわち

「この距離をこの時間で割ると」で表示している値 [m/s] を km/h に換算した結果を表示します。

### 一方、現在の速度は

現在列車が走行している速度を表示します。

### この速度で次のポイントに着くのは

列車が現在の速度で走り続けたとき次のポイントに着くことが予測されるタイミングと、
次のポイントに着くべきタイミングの差を表示します。
同時刻の場合は「定時」、着くことが予測されるタイミングの方が遅い場合は「○ 秒延」、
着くことが予測されるタイミングの方が早い場合は「○ 秒早」と表示します。
また、1000 秒以上遅れることが予測される場合は「∞ 秒延」と表示します。

## 使用上の注意

提示する速度やタイミングは単純な計算で求めており、あくまでも目安です。
信号を含む速度制限などの影響により、
表示される速度で走り続けることができない可能性があります。
また、次のポイントで停車する場合、
停車のための減速の影響で予測と実際の結果が大きくズレる可能性があります。

## 関連リンク

* 距離÷時間
  * https://github.com/mikecat/distance_per_time
* TRAIN CREW
  * https://acty-soft.com/traincrew/
  * https://store.steampowered.com/app/1618290/TRAIN_CREW/

## ライセンス

「距離÷時間」は、MITライセンスです。

```
Copyright (c) 2024 みけCAT

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

TrainCrewInput.dll (TRAIN CREW 入出力ライブラリ) は
溝月レイル/Acty様の制作物であり、MITライセンスの対象外です。
TrainCrewInput.dll の解析や改変は禁止されています。
