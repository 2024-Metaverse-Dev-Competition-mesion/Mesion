# 📖 Mesion
![image](https://github.com/user-attachments/assets/2edb4a38-d987-4568-9643-cc9003212a9f)

<br>

## 프로젝트 소개

- 플레이어는 초경량비행장치(드론) 조종자 증명취득을 위한 필기와 실기 시험을 도움받을 수 있습니다.
- 필기 시험은 지난 시험 문제 데이터를 기반하여 문제를 제공 받으며 풀이한 문제에 대해서 생성형 AI를 통한 문제 생성 및 문제 해설을 받을 수 있습니다.
- 실기 시험은 현실 실기 시험과 동일한 환경을 구현하여 자격증 취득에 도움을 줄 수 있습니다.
- 그 외에도 상점을 통한 이색 드론을 구매하여 XR공간에서 조종 할 수 있으며, 드론을 활용한 직업체험도 즐길 수 있습니다.

<br>

## 팀원 구성 및 역할 분담

### 팀원 전체
- 목표 시스템 시나리오 상세 설계
- 유니티를 이용한 개발 지원
- 버그 테스트 및 개선

<br>

### 김태경 (팀장)

- 프로젝트 총괄 및 파이프라인 구축
- 개인 공간 커스터마이징 기능 구현
- 상점 기능 구현
- XR 상호작용 개발
- 사용자 UI/UX 개발

<br>
    
### 김경보
- 생성형 ai 펫 심리 상담 기능 구현 및 개선
- 드론 실기 이륙 전 과정 개발
- 실기 모드 개발

<br>

### 이우진
- 유니티 블렌더 환경 디자인 구축 개발
- 객체 애니메이션 구현
- 생성형 ai를 통한 문제 생성 및 해설 기능 개발
- 필기 문제 출제 기능 개발
- 필기 모드 개발

<br>

### 최시은
- 드론 농업 직업 체험 시스템 구현 및 개발
- 사용자 UI/UX 개발
- 필기 모드 개발
- 시연 영상 제작

### 한승희

- 드론 화재 진압 직업 체험 시스템 구현 및 개발
- 드론 실기 이륙 전후 과정 개발
- 실기 모드 개발

<br>

## 경진대회 제출 영상
[시연 영상 보기](https://drive.google.com/file/d/1MSvkOxRyA1Y1OWhpdMjxKYXuCrXZOm0A/view?usp=drive_link)

<br>

## 1. 개발 환경

- Engine : Unity 5 (LTS 22.03.33f1)
- 프로그래밍 언어 : C#
- 생성형 AI API : OpenAI GPT3.5 API
- 버전 및 이슈관리 : Github
- 협업 툴 : Discord, Notion, Github
- 테스트 기기 : Meta Quest 3, Meta Quest 2
<br>

## 2. 채택한 개발 기술과 브랜치 및 Workspace 전략

### Unity 5

- Version
    - LTS 22.03.33f1
    - 개발 시작 기준 LTS 버전 중 가장 최신 버전을 이용하였습니다.
- Render Pipline
    - URP(Universal Render Pipline)
    - URP는 경량 렌더링 파이프라인이므로, 불필요한 그래픽 처리 없이 효율적으로 리소스를 활용해 높은 프레임률을 제공하기에 사용을 했습니다.
    
### 생성형 AI

- OpenAI GPT
    -  GPT의 경우 많은 데이터를 학습하였기에 생성형 AI에 사용해도 괜찮다고 생각하여 사용했습니다.
    -  GPT의 경우 범용성이 뛰어나기에 Mesion에서도 양질의 데이터 값을 줄 것이라 판단하여 사용했습니다.

### 브랜치 전략

- Git-flow 전략을 기반으로 main, develop, 기능별 브랜치를 사용하여 체계적으로 개발을 관리했습니다.
- main, develop, 기능 브랜치로 나누어 개발을 하였습니다.
    - **main** 브랜치: 배포 단계에서만 사용하는 브랜치로, 최종적으로 안정화된 코드만 포함됩니다.
    - **develop** 브랜치: 개발을 통합하는 브랜치로, 각 기능 브랜치에서 작업이 완료되면 여기로 병합됩니다.
    - **기능** 브랜치: 기능 단위로 독립적인 개발을 위한 브랜치로, 개발 완료 후 develop 브랜치로 병합해 통합 과정을 거칩니다.

### 워크스페이스 전략

- 5개의 독립된 워크스페이스를 각 팀원에게 배정하여, 개별 개발이 가능하도록 설계했습니다.
- main, develop, 각 기능 브랜치로 나누어 개발을 하였습니다.
    - **개별 워크스페이스 분리**의 이유: 동일한 폴더에서 작업 시, 각 팀원이 사용하지 않는 코드가 함께 병합되거나 충돌이 발생할 수 있어 이를 방지하려는 목적입니다. 각 팀원의 워크스페이스는 독립적으로 운영되므로, 다른 팀원이 개발하는 영역에 불필요한 영향이 없도록 했습니다.
    - **협업 방식**: 팀원 간 필요한 코드나 리소스가 있을 경우, 상대방의 워크스페이스에서 필요한 부분을 복사하거나 직접 사용하는 방식으로 협업했습니다.

<br>

## 3. 개발 기간, 경진대회 일정 및 작업 관리

### 개발 기간

- 전체 개발 기간 : 2024-06-14 ~ 2024-09-12

<br>

### 경진대회 일정
![image](https://github.com/user-attachments/assets/f441c2a1-35ca-4b2d-832c-5809db18e55a)

<br>

### 작업 관리

- GitHub를 사용하여 진행 상황을 공유했습니다.
- 매주 목, 일요일에 Discord를 통해 회의를 진행하여 현재 상황 내용을 공유했습니다.

<br>

## 4. 개발 세부 내용

### 개발 배경
 최근 산업 전반에서 드론 자격증에 대한 수요가 급격히 증가하고 있습니다. 그러나 현실적으로 드론 자격증을 취득하는 과정은 비용이 많이 들고 합격률이 낮아 많은 사람들이 어려움을 겪고 있습니다. 이러한 문제를 해결하기 위해, 저희는 메타버스 기반의 XR 프로그램을 개발하여 사용자들이 보다 저렴하고 효율적으로 드론 자격증을 준비할 수 있도록 돕고자 합니다. 이 프로그램은 실제 시험과 유사한 환경을 제공하여, 사용자가 실전에 가까운 경험을 쌓을 수 있도록 설계되었습니다.

### 개발 목적
 이 프로그램의 개발 목적은 드론 자격증 취득 과정에서의 높은 비용과 낮은 합격률로 인한 어려움을 완화하는 데 있습니다. 이를 위해 현실적인 훈련 환경을 제공하는 메타버스 XR 기술을 활용하여 사용자들이 보다 안전하고 효율적으로 실습할 수 있는 기회를 제공합니다. 궁극적으로 이 프로그램은 드론 조종 능력을 체계적으로 향상시키고 자격증 시험 준비를 보다 쉽게 지원함으로써 드론 산업의 인재 양성에 기여하는 것을 목표로 합니다. 단순히 드론 자격증 준비에 그치지 않고, 다양한 산업 분야에서 드론을 활용해볼 수 있는 직업 체험 기능을 통해 사용자들이 드론의 새로운 가능성을 탐구할 수 있도록 설계되었습니다. 또한 현실에서는 어려운 다양한 물체 조종 기능을 제공하여, 사용자들이 창의적으로 드론을 활용하는 방법을 탐구하고 새로운 가능성을 모색할 기회를 제공합니다. 이를 통해 드론을 통한 직업 세계를 체험하고 드론 조종 기술을 다방면으로 확장할 수 있는 환경을 제공합니다.

### 프로젝트 개발 세부 내용
저희 프로젝트는 다음의 주요 키워드를 중심으로 개발되고 있습니다:

- **진로 탐색 및 결정 지원**: 드론 자격증 취득과 관련된 과정을 통해 사용자가 다양한 직업을 가상으로 체험할 수 있게 돕습니다. 이를 통해 사용자들은 자신의 흥미와 적성에 맞는 진로를 발견하고, 실제 직업 세계를 경험하며 실질적인 직업 선택의 통찰을 얻을 수 있습니다.

- **생성형 AI 기반 필기시험 지원**: 과거 시험 데이터를 기반으로 문제 풀이가 가능하며, 생성형 AI를 통해 풀이한 문제에 대한 추가 해설과 새로운 문제 생성 기능을 제공하여 학습과 심화 학습을 지원합니다.

- **실제 실기 시험 프로세스 동일 구현**: VR 공간에서 실제 드론 실기 시험의 프로세스와 환경을 동일하게 구현하여, 사용자가 자연스럽게 시험 과정에 적응하고 준비할 수 있도록 지원합니다.

- **창의성 및 자아 표현 촉진**: XR 공간에서 사용자가 원하는 드론을 선택해 조종할 수 있으며, 이를 통해 창의적 사고와 자아 표현을 자유롭게 펼칠 수 있는 환경을 제공합니다.

- **타 서비스와의 차별점**: 추가 장비 없이 VR 기기만으로 플레이가 가능하며, 이색적인 드론 조종과 드론 직업 체험 콘텐츠가 포함되어 있습니다. 드론에 대한 이해도를 높일 수 있는 드론 부품의 이해와 조립 콘텐츠도 함께 제공됩니다.

<br>

## 5. 시스템 구성 아키텍쳐
![image](https://github.com/user-attachments/assets/2e455453-8b72-4e82-8c08-0042efd92961)

시스템 흐름은 다음과 같습니다. 
1. XR 기기를 통한 프로그램 실행
2. 패스스루 설정을 통한 환경 구축
3. 개인공간에서 드론을 자유로이 조종 가능
4. 직업 체험을 할 수 있는 캐주얼 모드
5. 드론자격증의 필기와 실기 시험을 준비 할 수 있는 리얼리티 모드
<br>

## 6. 프로젝트 주요 기능
### 개인 공간
| 진입화면 | XR 환경 내 드론 조종 | 핸드 메뉴 | 상점 |
|----------|----------|----------|----------|
|![진입 화면](https://github.com/user-attachments/assets/d5837fa2-dac8-42da-8911-758573f816d2)|![2 xr환경 내 드론 조종](https://github.com/user-attachments/assets/8a1a6e8e-e676-44eb-b1c2-0743023b9dcc)|![3 핸드메뉴](https://github.com/user-attachments/assets/2f85e2bf-baad-48ce-a0a4-4648134d27ce)|![4 상점](https://github.com/user-attachments/assets/4cdd4af7-5215-4527-9f3a-3f116850c4c5)|

- ##### 진입화면 : 패스스루 기능을 통해 현재 위치의 공간을 스캔한 후 프로그램에 진입합니다.
- ##### XR 환경 내 드론 조종 : XR 환경에서 자유롭게 드론을 조종할 수 있습니다.
- ##### 핸드 메뉴 : 자격증 및 인벤토리 확인이 가능하며, 드론의 캐주얼 모드(직업 체험)와 필기/실기 모드로 진입할 수 있습니다.
- ##### 상점 : 드론과 다양한 아이템을 구매할 수 있습니다.

<br>

### 드론 캐주얼 모드(직업 체험)
| 드론 화재 진압 | 드론 농업 |
|----------|----------|
|![드론 화재 진압](https://github.com/user-attachments/assets/7ed8f825-1b8f-440c-89f3-894f56131830)|![드론 농업](https://github.com/user-attachments/assets/b396c0cb-078e-40f1-85d0-45ac0c735978)|

- ##### 드론 화재 진압 : 드론을 조종해 건물 화재를 진압하는 미래 직업을 체험할 수 있습니다.
- ##### 드론 농업 : 드론을 조종해 씨앗을 뿌리고 물을 주는 농업 활동을 체험할 수 있습니다.

<br>

### 필기 모드
| 필기 모드 진입 화면 | 시험 모드 진입 | 시험 모드 | 문제 기록 |
|----------|----------|----------|----------|
|![5 필기모드 진입 화면](https://github.com/user-attachments/assets/8e106f6f-213d-4244-9760-2d2a549fb0f9)|![6 시험 모드 진입](https://github.com/user-attachments/assets/31afd023-eea8-4bcd-a086-cf8ba59ad1e0)|![7 시험 모드](https://github.com/user-attachments/assets/b3c1e96d-ad9b-4c5e-abdb-7589a3993aa0)|![8 문제 기록](https://github.com/user-attachments/assets/d6f06de7-91ba-4d57-a5cd-1bdc6c7ff084)|

- ##### 필기 모드 진입 화면 : 필기 모드로 진입하며, 간단한 설명을 제공합니다.
- ##### 시험 모드 진입 : 시험 모드로 진입하며, 실제 CBT 환경을 기반으로 시작됩니다.
- ##### 시험 모드 : 실제 CBT 환경에서 문제를 풀고, 제출 후 합격 여부를 확인할 수 있습니다.
- ##### 문제 기록 : 연습 및 시험 모드에서 문제를 풀고 제출하면 풀이 내역이 자동으로 기록됩니다.

| 연습 모드 진입 | 연습 모드 | 문제 해설 | 유사 문제 |
|----------|----------|----------|----------|
|![11 연습 모드 진입](https://github.com/user-attachments/assets/9d962b4c-3bb6-4616-9314-a3d53ea7eb07)|![12 연습 모드](https://github.com/user-attachments/assets/086f9b56-8855-48a9-bd82-48c38a6ba0e6)|![9 문제 해설](https://github.com/user-attachments/assets/a9556463-f3d4-4dbc-9d1e-bf7b76848957)|![10 유사 문제](https://github.com/user-attachments/assets/d2c43287-76fa-4822-bb4d-1f7585e694e7)|

- ##### 연습 모드 진입 : 연습 모드로 진입하며, '랜덤 1문제 풀기', '항공 법규', '항공 기상', '비행 이론 및 응용' 중 하나를 선택할 수 있습니다.
- ##### 연습 모드 : 선택한 모드에 따라 문제를 풀이할 수 있습니다.
    - 랜덤 1문제 풀기: 3개 파트 중 무작위로 문제가 출제되며, 각 문제 풀이 후 바로 정답 여부를 확인할 수 있습니다.
    - 파트별 선택: 선택한 파트의 문제 10개가 출제됩니다.
- ##### 문제 해설 : 생성형 AI를 통해 자신이 푼 문제에 대한 상세 해설을 제공합니다.
- ##### 유사 문제 : 생성형 AI를 활용해 유사한 문제를 추가로 풀 수 있으며, 풀이 후 즉시 정답 여부를 확인할 수 있습니다.
  
<br>

### 실기 모드
| 비행 준비 | 배터리 장착 | 기체 점검 | 조종기 전원 켜기 |
|----------|----------|----------|----------|
|![13 비행 준비](https://github.com/user-attachments/assets/87e6abf5-05d2-4d44-9b69-51efd17a3d70)|![14 배터리 장착](https://github.com/user-attachments/assets/89d6c544-1e53-4eb3-b112-ea0b7ffc343e)|![15 기체 점검](https://github.com/user-attachments/assets/a45d2421-7071-40f0-8dab-21758ef5b731)|![16 조종기 전원 키기](https://github.com/user-attachments/assets/85b0ce54-da32-4a14-afd4-08204899ef0d)|

- ##### 비행 준비 : 드론 위치로 이동하여 비행 준비를 시작합니다.
- ##### 배터리 장착 : 드론에 배터리를 장착합니다.
- ##### 기체 점검 : UI의 체크리스트를 통해 드론의 전반적인 상태를 점검하고 학습합니다.
- ##### 조종기 전원 켜기 : 컨트롤러를 사용하여 조종기의 전원을 켭니다.

| 이륙 비행 실시 | 정지 호버링 실시 | 전진 및 후진 비행 실시 | 삼각비행 실시 |
|----------|----------|----------|----------|
|![17 이륙 비행 실시](https://github.com/user-attachments/assets/56569250-ac2b-454d-b885-1ab0893b797a)|![18 정지 호버링 실시](https://github.com/user-attachments/assets/80770d7f-2e9f-473b-8437-e319712c6b30)|![19 전진 및 후진 비행 실시](https://github.com/user-attachments/assets/33b4e2dc-ebd6-4c2e-a55a-cffc1fa94bfb)|![20 삼각비행 실시](https://github.com/user-attachments/assets/6e4293b1-477e-4023-82bb-d2debffc1b48)|

- ##### 이륙 비행 실시 : 이륙 비행 과정을 진행하며, UI를 통해 전반적인 이륙 비행 과정을 학습니다.
- ##### 정지 호버링 실시 : 정지 호버링 과정을 진행하며, UI를 통해 전반적인 정지 호버링 과정을 학습니다.
- ##### 전진 및 후진 비행 실시 : 전진 및 후진 비행 과정을 진행하며, UI를 통해 전반적인 전진 및 후진 비행 과정을 학습니다.
- ##### 삼각비행 실시 : 삼각비행 과정을 진행하며, UI를 통해 전반적인 삼각비행 과정을 학습니다.
  
| 원주비행 실시 | 비상조작 실시 | 정상접근 및 착륙실시 | 추풍접근 및 착륙실시 |
|----------|----------|----------|----------|
|![21 원주비행 실시](https://github.com/user-attachments/assets/88f28abd-51b4-4091-be40-ba6c34a25ad9)|![22 비상조작 실시](https://github.com/user-attachments/assets/85902f3b-8a14-45e4-92b2-4d49f7123f81)|![23 정상접근 및 착률실시](https://github.com/user-attachments/assets/0cf77267-efa7-4d0c-8220-2627dfd153f3)|![24 축풍접근 및 착륙실시](https://github.com/user-attachments/assets/a590e91f-cc01-4b7b-8b15-efe410218360)|

- ##### 원주비행 실시 : 직접 원주 비행을 수행하며, UI 가이드를 통해 전체 비행 절차를 익힙니다.
- ##### 비상조작 실시 : 비상 상황을 가정하고 드론을 조종하여 비상 조작을 경험하며, UI 안내에 따라 각 절차를 학습합니다.
- ##### 정상접근 및 착륙실시 : 정상 접근과 착륙을 실습하며, UI 가이드와 함께 안전한 착륙 절차를 익힙니다.
- ##### 추풍접근 및 착륙실시 : 바람이 있는 상황에서 접근 및 착륙을 체험하며, UI를 통해 실전 같은 절차를 학습합니다.
  
<br>

## 7. 기대 효과 및 활용 분야

### 기대 효과
- **드론 자격증 취득 효율성 향상**: 현실적인 VR 실습을 통해 실기 시험에 대한 적응력과 준비도를 크게 향상시켜 자격증 취득 성공률을 높일 수 있습니다. 생성형 AI 기반 필기시험 학습 지원을 통해 사용자는 깊이 있는 학습이 가능하며, 필기시험의 합격률 또한 증가할 것으로 기대됩니다.
- **진로 탐색 및 결정의 효과적 지원**: 가상 직업 체험을 통해 청소년들이 다양한 드론 관련 직업을 경험하면서 자신의 흥미와 적성에 맞는 진로를 보다 정확하게 탐색할 수 있습니다. 이를 통해 직업의 장단점을 이해하고, 실질적인 진로 결정을 내릴 수 있는 기회를 제공합니다.
- **산업 현장에서의 실질적 역량 강화**: VR을 통해 실제와 유사한 환경에서 드론 조종 능력을 키우고, 다양한 상황을 시뮬레이션하여 실무 능력을 향상시킬 수 있습니다. 현실에서 접근이 어려운 복잡한 드론 조종을 가상 환경에서 경험함으로써 조종 역량을 극대화할 수 있습니다.
- **창의력 및 문제 해결 능력 향상**: XR 공간에서 사용자가 창의적으로 드론을 조종하고 다양한 물체를 가상으로 다루며 문제 해결 능력을 키울 수 있습니다.

### 활용 분야
- **교육 및 훈련**: 드론 자격증을 준비하는 교육 기관에서 교재 및 실습 도구로 활용할 수 있으며, 드론 운용이 필요한 기업, 군, 경찰 등에서 인력 훈련 도구로 사용할 수 있습니다.
- **직업 체험 및 진로 교육**: 청소년 진로 탐색 프로그램에서 드론 관련 직업 체험을 제공하여 진로 교육에 기여할 수 있으며, 직업 체험관이나 교육 박람회에서도 활용 가능합니다.
- **산업 및 상업적 활용**: 건설, 물류, 농업, 재난 구조 등 다양한 분야에서 드론 조종 및 운용 능력을 갖춘 인력 양성에 사용될 수 있으며, 기업 내 신입사원 훈련과 직무 역량 강화에도 적용할 수 있습니다.
- **창의적 콘텐츠 개발**: XR 환경에서 다양한 드론 조종 경험을 바탕으로 창의적인 콘텐츠 제작 및 개발에도 활용될 수 있습니다.
