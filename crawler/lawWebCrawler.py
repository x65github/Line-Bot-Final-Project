#!/usr/bin/env python
# coding: utf-8

# In[109]:


import requests
from bs4 import BeautifulSoup
import time
import argparse
# import pandas as pd

# 爬蟲將條目跟內容抓下來
def crawlerDetail(law, catalog):
    
    time.sleep(2)
    res = requests.get('https://law.moj.gov.tw/LawClass/LawAll.aspx?pcode='+law)
    soup = BeautifulSoup(res.text, "html.parser")

    numberAll = soup.select('.row > .col-no')
    detailAll = soup.select('.row > .col-data > .law-article')
    numberClean = []
    detailList = []

    # 清整條目與內容
    if law.find('A') != -1 :
        numberAll.pop(0)
        detailAll.pop(0)
        
    for i in range(len(numberAll)):
        number = str(numberAll[i]).split('">')[2].split('</a><')[0]
        numberClean.append(number)

    for j in range(len(detailAll)):
        detail = str(detailAll[j]).split('"law-article">')[1].split('</div></div>')[0]
        detailList.append(detail)

    # 依號碼尋找對應內容
    if catalog in numberClean:
        detailText = detailList[numberClean.index(catalog)]
    else:
        answer = '無法依據法條號碼查到內容'
    
    # 依內容判斷是否換行或是否編號
    detailSmall = []

    detailText = detailText.replace('</div>', '').replace('\n', '')
    if detailText.find('<div class="line-0004">') != -1:
        detailText = detailText.replace('<div class="line-0004">', '\n')

    if detailText.find('<div class="line-0000 show-number">') != -1:
        detailSmall = detailText.split('<div class="line-0000 show-number">')

        detailSmall.remove('')
        for i in range(len(detailSmall)):
            detailSmall[i] = str(i+1) + '. ' + detailSmall[i]
        answer = '\n'.join(detailSmall)
    else:
        answer = detailText.replace('<div class="line-0000">', '')
    return answer


# In[111]:


def crawler(numberInput):
    
    # 製作全形半形陣列
    halfList = []
    fullList = []
    sameHalfList = []
    sameFullList = []
    for i in range(33,127):
        halfList.append(chr(i))
        fullList.append(chr(i+65248))
        
    # numberInput = '民法 245-1'
    catalogList = []
    catalog = ''
    answer = ''

    # 確認輸入格式正確
    if numberInput.find(' ') != -1:
        lawInput = numberInput.split(' ')[0]
        catalogInput = numberInput.split(' ')[1]
        print(lawInput + catalogInput)
        for i in range(len(catalogInput)):
            catalogList.append(catalogInput[i])
        sameHalfList = [a for a in catalogList if a in halfList]
        sameFullList = [a for a in catalogList if a in fullList]
        
        # 檢查全形半形
        if len(sameHalfList) > 0 and len(sameFullList) == 0:
            catalog = '第 ' + catalogInput + ' 條'
            
            
#             df = pd.read_excel(r".\law.xlsx")
            lawCodeDict = {'憲法': 'A0000001',
                             '行程': 'A0030055',
                             '行訴': 'A0030154',
                             '憲訴': 'A0030159',
                             '國民法官': 'A0030320',
                             '地制': 'A0040003',
                             '民法': 'B0000001',
                             '涉民': 'B0000007',
                             '民訴': 'B0010001',
                             '非訟': 'B0010008',
                             '家事': 'B001004',
                             '刑法': 'C0000001',
                             '刑訴': 'C0010001',
                             '海洋': 'D0090064',
                             '票據': 'G0380028',
                             '證交': 'G0400001',
                             '信託': 'I0020024',
                             '商標': 'J0070001',
                             '專利': 'J0070007',
                             '著作': 'J0070017',
                             '公司': 'J0080001',
                             '營業': 'J0080028',
                             '消保': 'J0170001',
                             '海商': 'K0070002',
                             '藥害': 'L0030023',
                             '勞基': 'N0030001',
                             '國公': 'Y0000039'}
#             for i, row in df.iterrows():
#                 lawCodeDict[row['使用者輸入'].replace('\xa0','')] = row['pcode']
                
            try:
                answer = crawlerDetail(lawCodeDict[lawInput], catalog)
            except:
                answer = '好像沒有這個法律'
                                
        else:
            answer = '好像沒有照著格式輸入欸，請再輸入一次(只接受半形，需空格)'
    else:
        answer = '好像沒有照著格式輸入欸，請再輸入一次(只接受半形，需空格)'
    return answer

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument('--law', type=str, default='')
    args = parser.parse_args()
    law = args.law
    print(crawler(law))
#     print(crawler('涉民 18'))


# In[52]:


# print(answer)


# In[ ]:




