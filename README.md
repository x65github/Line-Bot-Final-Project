# 法條即時通

## 環境設置

### Line-Chatbot
使用C#作為主要撰寫程式，套用python蒐集特定頁面資訊，透過Line-chat-bot回應該法條內容。


### 歡迎訊息
<table>
  <tr>
    <td>input</td>
    <td>無，收到follow event時觸發</td>
  </tr>
  <tr>
    <td>output</td>
    <td>歡迎加入2022 Side Project<br>
	本機器人尚在建置中<br>
	目前的機器人種類：法律查詢機器人<br>
	===使用規則===<br>
	輸入：<br>
	法規名稱 條號，如：民法 1<br>
	輸出：<br>
	法條內容，如：民事，法律所未規定者，依習慣；無習慣者，依法理。</td>
  </tr>
</table>

### 使用規則 


<table>
  <tr>
    <td colspan="2" align="center" valign="center">input</td>
    <td colspan="2" align="center" valign="center">output</td>
  </tr>
  <tr align="center" valign="center">
    <td>規則</td>
    <td>範例</td>
    <td>規則</td>
    <td>範例</td>
    <td>備註</td>
  </tr>
  <tr align="center" valign="center"><h6>
    <td><h6>法規簡稱+空格+條號</h6></td>
    <td>民法 1</td>
    <td>法條內容</td>
    <td>民事，法律所未規定者，依習慣；無習慣者，依法理。</td>
    <td> </td>
  </tr>
  <tr align="center" valign="center">
    <td>help<br>/使用規則</td>
    <td>Help</td>
    <td>使用說明</td>
    <td align="left" valign="center">===使用規則===<br>  
      輸入：法規名稱 條號，如：民法 1<br>
      輸出：法條內容，如：民事，法律所未規定者，依習慣；無習慣者，依法理。</td>
    <td>無分大小寫</td>
  </tr>
  <tr>
    <td>錯誤格式或訊息</td>
    <td>刑法a</td>
    <td>錯誤訊息</td>
    <td>好像沒有照著格式輸入欸，請再輸入一次(只接受半形，需空格)</td>
    <td></td>
  </tr>
</table>

  
## 程式流程圖
   <img src="https://github.com/x65github/Line-Chat-Bot/blob/main/linebot%E5%B0%88%E6%A1%88%E6%B5%81%E7%A8%8B%E5%9C%96.drawio.png" alt="image" style="max-width: 100%;">
   
## EXE流程圖
   <img src="https://github.com/x65github/Line-Chat-Bot/blob/main/exeintrodaction.png" alt="image" style="max-width: 100%;">

## 參考法源
<table>
  <tr>
	  <td align="center" valign="center">table
	  <td>使用者輸入</td><td>pcode</td></tr>
  <tr>
	 <td rowspan="4" align="center" valign="center">一年級<br>
		<h6>列出<i>母法</i><br>
		一年級僅學習總則</h6>
	</td>
	<td>憲法</td><td>A0000001</td>
  </tr>
  <tr><td>行程</td><td>A0030055</td></tr>
  <tr><td>行訴</td><td>A0030154</td></tr>
  <tr><td>憲訴 </td><td>A0030159</td></tr>
  <tr>
	<td rowspan="6" align="center" valign="center">二年級</td>
	<td>國民法官</td><td>A0030320</td></tr>
  <tr><td>地制</td><td>A0040003</td></tr>
  <tr><td>民法 </td><td>B0000001</td></tr>
  <tr><td>涉民</td><td>B0000007</td></tr>
  <tr><td>民訴</td><td>B0010001</td></tr>
  <tr><td>非訟</td><td>B0010008</td></tr>
  <tr>
	<td rowspan="6" align="center" valign="center">三年級</td>
	<td>家事</td><td>B001004</td></tr>
  <tr><td>刑法</td><td>C0000001</td></tr>
  <tr><td>刑訴</td><td>C0010001</td></tr>
  <tr><td>海洋</td><td>D0090064</td></tr>
  <tr><td>票據</td><td>G0380028</td></tr>
  <tr><td>證交</td><td>G0400001</td></tr>
  <tr>
	<td rowspan="8" align="center" valign="center">四年級</td>
	<td>信託</td><td>I0020024</td></tr>
  <tr><td>商標</td><td>J0070001</td></tr>
  <tr><td>專利</td><td>J0070007</td></tr>
  <tr><td>著作</td><td>J0070017</td></tr>
  <tr><td>公司</td><td>J0080001</td></tr>
  <tr><td>營業</td><td>J0080028</td></tr>
  <tr><td>消保</td><td>J0170001</td></tr>
  <tr><td>海商</td><td>K0070002</td></tr>
  <tr>
	<td rowspan="3" align="center" valign="center">如果你還沒畢業</td>
	<td>藥害</td><td>L0030023</td></tr>
  <tr><td>勞基</td><td>N0030001</td></tr>
  <tr><td>國公</td><td>Y0000039</td></tr>
<table/>

取自 [全國法規資料庫](https://law.moj.gov.tw/Index.aspx)
