<div align="center">
  
![브로셔 로고 최종](https://github.com/user-attachments/assets/44a0336c-ca49-4701-9228-c049d4449bcd)

## 📸 프로젝트 결과물 소개 📸

### 📹 트레일러 영상 📹
<br>

[![Video Title](https://github.com/user-attachments/assets/29d368fd-5ad0-4ed5-b446-83e275e92936)
](https://www.youtube.com/watch?v=TJG4Vq6jI74)


<br>
<br>
<br>


### 🔎 브로셔 🔎
<br>

![브로셔 타이틀](https://github.com/user-attachments/assets/a1fd175a-28ce-450b-9255-5e203028fda2)



</div>


## 📜 프로젝트 개요 및 목표 📜

### **🎯 게임의 목표**
> 1. 게임 진입 시 랜덤으로 배치된 맵이 등장<br><br>
>     - 맵에는 일반 몹 전투, 사건, 보스 전투, 휴식, 상점으로 이루어짐<br><br>
>     - 필요한 카드를 획득하고, 강화하며 나만의 덱을 완성해 나갈 수 있음<br><br>
> 2. 플레이어는 원하는 장면으로 진입하여 보상을 얻으며, 보스를 무찌르는 것이 목표!
<br>
<br>

### **🎯 게임의 세 가지 컨셉 구성**
>
> 1. 2.5D의 맵 구성
>    
>    - 3D맵과 2D플레이어를 이용하여 구현
>    - 맵을 탐색하며 나아가는 분위기를 조성
>
> 2. 턴제 카드 배틀
>    
>    - 적의 행동에 맞춰 내가 만든 덱으로 상황 대처
>    - 나만의 전략으로 적들을 물리치기!
>
> 3. 픽셀풍 아트 디자인
>    
>    - Voxel과 픽셀 sprite로 이루어져 단순하지만 매력적인 분위기

## 📜 Tutorial 📜
![tutorial1](https://github.com/user-attachments/assets/01ae87ed-6919-4a7a-9a2b-a16aa97c756e)
![tutorial2](https://github.com/user-attachments/assets/da23aa2b-d462-4bae-a132-5965f9ef79e9)
![tutorial3](https://github.com/user-attachments/assets/024c5736-40c5-4d40-ae1e-95b97060f58d)



## 📒 기술적인 도전 과제 📒

    

> ### **필수 구현 기능**
> 
1. 전투 시스템 
    ![전투 기능](https://github.com/user-attachments/assets/8e84326a-df93-443a-82b6-0e582b34a8b0)

2. 사건 기능 
    ![영상11](https://github.com/user-attachments/assets/9b642faa-e798-4a28-8b30-014026b1d2ad)

3. 휴식 기능 
    ![휴식기능](https://github.com/user-attachments/assets/a1f6dd17-065e-4113-9781-17a53cabe7bc)

    
4. 상점 기능
    ![영상12](https://github.com/user-attachments/assets/5aed93bd-8450-44cc-bc0d-e1358add775f)

    

> ### **선택 구현 기능**
> 
1. 카드 강화 기능
    ![영상6](https://github.com/user-attachments/assets/c4993c59-f5b3-46e9-8f9e-9edc6490326b)

2. 보스 몬스터 추가    
3. 랜덤 맵 생성
4. 신규카드 획득기능 추가

<br>
<br>

## 💻사용된 기술 스택 💻

Unity 2022.3.17f1 버전, C#, Git hub
<br>
<br>
와이어프레임, 개발 회의 https://www.figma.com/board/sltFxd8NaJVqwaPYSZrYKq/Proj.육망성의-앙마들?node-id=0-1
<br>
<br>
작업 현황 공유 https://www.notion.so/teamsparta/d09ffb0879ef402f8ae988543159f7eb

<br>
<br>

## 🧱 클라이언트 구조 🧱

### 💡 **Game Flow**
  
![Game Flow](https://github.com/user-attachments/assets/852c7b66-0c2f-4f48-9b00-e32337c33f89)

<br>
<br>

### 💡 **프로젝트 구조**

![image](https://github.com/user-attachments/assets/b827f2b7-e0ec-480d-8393-a86698c74f80)

<br>
<br>

### 💡 **CSV파일 종류와 활용**

![image (16)](https://github.com/user-attachments/assets/825e0680-5f1e-4514-b0e9-f1c074adf87c)


 CardData 컨벤션에 따라서 각기 다른 string의 키 값을 부여받음.
<br>
<br>
- Card 컨벤션
    
    
    | CardCode | Name | Cost | Describle | Elemental | Damage | Shield | Type | Weak | Strength | Poison | Mana | Draw |
    | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
    | 카드의 고유번호 | 이름 | 비용 | 설명란 | 속성 | 데미지 | 쉴드 수치 | 사용타입 | 약화 | 강화 | 독 | 마나 수치 | 드로우 |
<br>
<br>

### **📌 데이터 파싱**

CSV파일의 역직렬화 → DataManager

```csharp
datamanager코드 일부 작성
for (int i = 0; i < data_list.Count; i++)
{
    CardData cardData = new();

    cardData.CardCode = int.Parse(data_list[i][KeyWordManager.str_CardCode].ToString());
    cardData.Name = data_list[i][KeyWordManager.str_CardName].ToString();
    cardData.Cost = int.Parse(data_list[i][KeyWordManager.str_CardCost].ToString());
    cardData.Describle = data_list[i][KeyWordManager.str_CardDescrible].ToString();
    cardData.Elemental = (Elemental)int.Parse(data_list[i][KeyWordManager.str_CardElemental].ToString());
    cardData.Damage = int.Parse(data_list[i][KeyWordManager.str_CardDamage].ToString());
    cardData.Shield = int.Parse(data_list[i][KeyWordManager.str_CardShield].ToString());
    cardData.Type = int.Parse(data_list[i][KeyWordManager.str_CardType].ToString());
    cardData.Effect = new int[3];
    cardData.Effect[0] = int.Parse(data_list[i][KeyWordManager.str_CardEffectWeak].ToString());
    cardData.Effect[1] = int.Parse(data_list[i][KeyWordManager.str_CardEffectStrength].ToString());
    cardData.Effect[2] = int.Parse(data_list[i][KeyWordManager.str_CardEffectPoison].ToString());
  
    _cardList.Add(cardData);
}
```

→ Key와 Value를 통한 CSV 데이터 접근
<br>
<br>

### 💡 **캐릭터 관리**

- **`이곳에 baseCharacter의 내용을 간략히`**
    
    ```csharp
    public class BaseCharacter : MonoBehaviour
    {
        [Header ("현재 체력, 최대 체력")]
        public int hp;
        public int fullhp;
        [Header("쉴드 수치")]
        public int shield = 0;
        public Dictionary<StatusEffect, int> activeEffects = new Dictionary<StatusEffect, int>();
        public List<Elemental> currentElementalEffect = new List<Elemental>();
    
        public Animator EffectAnimator;
    
        public Transform ShakeTransform;   
    
        public Transform particlePosition;
    
        public virtual void StartTurn()
        
        public void HpDecrease(int damage, BaseCharacter target)
        
        public void Heal(int amount)
        
        public void AddShield(int amount)
        
        public void ApplyEffect(int[] stack, BaseCharacter target)
        
        protected virtual void ApplyPoisonDamage(BaseCharacter target)
        
        protected void DecreaseEffectStacks()
        
        public void RemoveStatusEffect(StatusEffect effect)
        
        public virtual void CheckDamage(int damage, BaseCharacter target)
        
        public virtual void OnAttacked(int damage, BaseCharacter target)
        
        public virtual void ApplyElementalEffect(int sheld, int damage, int[] effect, Elemental element, BaseCharacter target)
        
        protected void CheckElementalSynergy(int sheld, int damage, Elemental firstelement, Elemental secondelement, BaseCharacter taget)
        
        public void RemoveElementalEffect(BaseCharacter taget)
        
        public void LogCurrentEffects()
        
        private void ShakeObject(BaseCharacter target)
        
        private void Elemental_FireWater(int sheld, int damage, BaseCharacter target)
        
        private void Elemental_WaterThunder(int sheld, int damage, BaseCharacter target)
        
        private void Elemental_FireThunder(int sheld, int damage, BaseCharacter target)
        
        private void ApplySpecificEffect(StatusEffect effect, int stack, BaseCharacter target)
        
    }    
    ```
    

플레이어와 다른 몬스터들은 baseCharacter를 상속받음.

<br>
<br>

### 💡 **Battle Scene 관리**



- **`적 턴 Coroutine (BattleManager.cs)`**
    
    ```csharp
        IEnumerator EnemyTurnRoute()
        {
            yield return StartCoroutine(turnPanel.TurnChangeCoroutine());
    
            for (int i = EnemyList.Count - 1; i >= 0; i--)
            {
                count = EnemyList.Count;
                EnemyList[i].StartTurn();
    
                if (EnemyList.Count == count)
                {
                    while (EnemyList[i].IsAnimating)
                    {
                        yield return null;
                    }
                    yield return new WaitForSeconds(KeyWordManager.flt_EnemyActionInterval);
                }
            }
            PlayerTurn();
        }.
    ```
    
1. 스테이지의 적 객체는 List로 관리
2. List에서 순서대로 적의 행동 진행
3. 적의 애니메이션이 진행되는동안 return null으로 다음 적 행동지연
4. 모든 적의 행동이 끝나면 PlayerTurn진행

❗ 플레이어의 카드 사용은 BattleManager에서 구현

<br>
<br>


## 🙇🏻 사용자 개선 사항 🙇🏻

![image](https://github.com/user-attachments/assets/7856f6a8-1228-4b2f-b6f4-df5b417077f8)


### **버그 및 개선점**

1. **버그**:
    - 보스의 위치가 전투 중 점점 바뀜
    - 승리 후 카드 추가 선택 시 클릭한 만큼 카드가 들어옴(수정 완료)
    - 맵 이동 시 벽이 뚫려 빈 공간으로 떨어지는 오류(수정 완료)
    - 전투 진입 후 설정을 통해 메인 메뉴로 나갔다가 Load Game을 하면 해당 전투를 완료한 것으로 인식하는 오류

1. **난이도 조정**:
    - 보스 난이도 및 적 행동의 불균형 문제
    - 게임 난이도를 조정하여 더 도전적이고 균형 잡힌 경험 필요
2. **튜토리얼 및 설명 부족**:
    - 원소 시스템, 카드 드로우 및 소멸 카드에 대한 설명 부족
    - 게임 조작법 및 시스템에 대한 명확한 설명 필요
<br>
<br>

**추가 의견**

1. **게임 내용**:
    - 다양한 카드와 적 AI 패턴 개선 > 다양한 기능의 카드 추가 + 적 패턴 개선
    - 스토리 및 배경 설명 추가
    - 몬스터 외형 및 행동 패턴 다양화 필요   >> 패턴 추가
2. **기타**:
    - 게임의 전반적인 밸런스를 조절
    - 튜토리얼과 도움말 강화  >> 도움말을 더 잘보이게 수정
    - 데미지 표기 및 공격 모션 개선  >> 데미지 표시 텍스트 추가 및 공격 모션 개
3. **UI 및 디자인**:
    - 카드 설명 누락, 상점 UI 비효율, 게임 설정 부족
    - 화면 크기 문제, 그래픽 통일성 부족
4. **게임 시스템 개선**:
    - 카드 강화 및 합성 시스템 추가 필요
    - 원소 시너지 밸런스 조정, 다양한 카드 및 몬스터 추가
<br>
<br>

## 🥊 트러블 슈팅 🥊

- ### 1차 - 예외 처리
    ![image (17)](https://github.com/user-attachments/assets/a7e64d50-d62e-4187-ac3e-ec8fc1c32ed2)

    **`문제1`** : Foreach문에서 List내부 객체가 변경되었을 때 예외 처리를 요구하는 오류를 발견했습니다
    
    **`문제2`** : 해당 오류는 적의 턴이 시작되고, 몬스터가 독 데미지로 인해 사망했을 때 발생했습니다
    
    **`해결`** : 코드 상으로 EnemyList내부 Enemy객체를 지우기 때문에 발생한 것으로 추정되어, for문과 예외 처리를 통해 해결했습니다
    
    **`결과`** : foreach문으로 내부 객체를 지우거나, index번호를 바꾸는 경우에는 for문이 더욱 적합합니다
<br>
<br>

- ### 2차 - 데이터 파싱
    ![image (18)](https://github.com/user-attachments/assets/2f7d57b4-1237-4799-8eae-b035a45c3373)
  <br>
  ㄴ몬스터 데이터
  <br>
    ![image (19)](https://github.com/user-attachments/assets/549e7d21-d399-4328-a789-bd268887c49a)
  <br>
  ㄴ카드 데이터
  <br>
  
    **`문제`** : 프로젝트 진행 중 전투 로직을 완성 후 병합 과정에서 카드 데이터, 적 데이터의 파싱 부분 형식이 다르게 구현되어 같이 사용하는 전투  로직을 사용하는데 문제가 생김
    
    **`해결`** : 카드 데이터, 적 데이터를 구현한 팀원끼리 모여서 회의를 통해 형식을 통일함
   <br>
   <br>
   
  ![image (21)](https://github.com/user-attachments/assets/ece8fa4a-ecf6-4198-84e2-e7db215036ce)
  <br>
  ㄴ몬스터 데이터
  <br>
  ![image (20)](https://github.com/user-attachments/assets/43054d84-1ace-40ed-ba92-4a2dd01bfed2)
  <br>
  ㄴ카드 데이터
  <br>
    
    **`결과`** : 파싱 부분의 형식을 통일해서 구현
  
<br>
<br>    

- ### 3차 - 오브젝트 풀  

    
    **`문제1`** : 오브젝트에 있는 자식 오브젝트를 다른 자식 오브젝트로 옮긴 뒤 다시 오브젝트 풀로, 자식으로 설정할 때 반 정도 남는 현상이 있었습니다.
    
    **`해결1`** :  전체 크기가 변화하는 것을 막기 위해서 전체 크기를 보관하는 변수를 하나 만들고 앞에서 하나씩 빼는 방식으로 바꾸었습니다.
    
    **`결과1`** : for문으로 전체사이즈가 변경이 되어도 기존에 전체 크기가 담기 변수로 통해서 오브젝트가 빠져도 크기가 변경되지 않아 모든 자식오브젝트가 오브젝트 풀로 넘어갔습니다.
  <br><br>
    ![image (22)](https://github.com/user-attachments/assets/737c4ef3-6a75-42ff-9c28-d2fe4817fd0d)

    
    **`문제2`** : 상점 창에서 구매할 카드가 있는 상태에서 계속 플레이어 덱을 누르거나 혹은 Delete 카드에 창을 반복적으로 껐다 켜는 것을 반복하면 상점 창에 카드가 사라지는 현상이 있었습니다.
    
    **`해결2`** :  오브젝트풀이 원형 큐형태라는 것을 알고 중간에 활성화 된 것은 통과 되는 코드를 작성하였습니다. 
    
    **`결과2`** : 오브젝트 풀에서 오브젝트가 비활성화 되어있는지 확인하면서 활성화 되어있는 것은 다시 큐에 넣어서 다시 비활성화 되어있는 것을 찾게 만들었습니다.
  <br><br>
  
    ![image (23)](https://github.com/user-attachments/assets/838a64e2-f1ec-43da-873a-e50755bd10a4)
    
<br>
<br>   

- ### 4차 - 카드 핸드
    
    **`문제`** : 카드 핸드에서 중간에 있는 카드들은 hover가 안되고, 양 끝에 있는 카드들만 hover가 되는 오류입니다.<br> <br> 
    ![image (24)](https://github.com/user-attachments/assets/c69f2ac9-e0c5-439b-bc8c-7010ffe56f26)<br> 

    코드에서 OnPointerEnter 메서드는 객체가 목표 위치에 얼마나 가까운지를 평가하고, 이를 기반으로 hover 효과를 적용합니다. 이 과정에서 **타이밍 문제와 정확성 문제,** 두 가지 주요한 문제가 발생한 것으로 추측했습니다.
    
    **`해결`** :  <br> <br> 
    ![image (25)](https://github.com/user-attachments/assets/fb169290-97e6-4edc-91d1-e906a25e531a)<br> 

    코드는 hasReachedTarget라는 상태 변수를 도입하여 위 문제를 해결습니다. 애니메이션이 완료될 때마다 변수를 true로 설정하고, OnPointerEnter에서 이 상태를 확인하여 작동합니다.
    
    **`결과`** : 이로 인해 **더 명확한 상태 관리와 더욱 안정적인 동작,** 두 가지 개선이 이루어 졌다. 이와 같은 변경은 첫 번째 코드에서 발생했던 문제를 해결하고, 모든 카드에 대해 일관되고 안정적으로 hover 상태가 적용되었습니다.
    
<br>
<br> 

- ### 5차 - 배지어 커브

   ![카드Aiming 테스트 (1) (1)](https://github.com/user-attachments/assets/41ab6280-ae42-4932-903e-9506984134a0)
    <br> <br> 
    
    **`문제 1`** : 베지어 곡선을 그리는 것은 성공했지만, 현재 그리는 위치가 Canvas가 아닌 Transform의 영역에 있습니다. 
    기존에 사용된 `Line Renderer` 컴포넌트는 UI Canvas에서 사용할 수 없어, RectTransform 영역에서 곡선을 그릴 수 없습니다.

    **`해결`** : UI에서 선(Line)을 그릴 수 있는 방법을 구글링하여 적절한 코드를 찾은 후, 약간 수정하여 사용합니다. 
    선의 형태는 마우스 위치에 따라 실시간으로 변경되어야 하므로 `Update` 메서드에서 `SetVerticesDirty();`를 호출하여 선이 업데이트되도록 합니다. 
    `SetVerticesDirty();` 메서드는 `Graphic` 클래스의 `OnPopulateMesh`를 호출하여 UI에서 선을 다시 그리게 합니다.
    
    **`문제 2` :** 베지어 곡선을 그리기 위해 시작점과 중간점은 미리 정해두고, 끝점은 마우스 위치를 기준으로 결정하는 알고리즘을 사용했습니다.
    그러나 시작점과 중간점이 `RectTransform`에 속해 있더라도, 마우스 위치가 다른 좌표계(`Transform`)에 있다면 올바르지 않은 값이 나올 수 있습니다.
    따라서, 모든 좌표가 동일한 좌표계에서 계산되도록 해야 합니다.

    
  ```csharp
  public Canvas canvas;
  private RectTransform _canvasRect;
  private Vector2 _localPosition;
      .
      .
      .
  _canvasRect = canvas.transform as RectTransform;
      
  RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect,Input.mousePosition, canvas.worldCamera, out _localPosition);
  ```

    `RectTransformUtility.ScreenPointToLocalPointInRectangle` 메서드를 사용하여, 화면의 마우스 위치를 `Canvas` 좌표계로 변환합니다.
     이렇게 변환된 `_localPosition`을 사용하면, 마우스 위치가 항상 Canvas의 좌표계에 맞게 계산되므로 정확한 값이 나오게 됩니다.

    ![베지어 커브 수정본 (1)](https://github.com/user-attachments/assets/c8e0ecac-61aa-4596-bd6c-01c00b4c3f00)



<br>
<br> 

- ### 6차 - 적 행동 애니메이션 타이밍 문제
    
    **`문제`** : 적의 행동과 데미지 판정, 다음 적 행동 간의 타이밍이 맞지 않음
    
    - 플레이어가 피해를 먼저 받은 뒤에 애니메이션이 실행되었음
    
    **`해결`**  : 다음과 같은 순서로 프로그램이 진행되도록 수정함
    
    1. Trigger를 통한 애니메이션 실행       
    
    ![image (26)](https://github.com/user-attachments/assets/0f82a149-0f03-47c8-a71c-df68887f3565)
  
    <br>
    <br>
    
    2. 애니메이션에서 Event호출      
    
    ![image (27)](https://github.com/user-attachments/assets/58f82fc8-b59c-471f-80f4-cc9e120d254d)

    <br>
    <br>
    
    3. Event를 받아 데미지 판정
    
    ![image (28)](https://github.com/user-attachments/assets/8e0d6e7f-446a-4dd0-81c3-2606f9415690)

  
    <br>
    <br>
    
    4. IsAnimating bool값을 통한 코루틴 제어

    
    ```csharp
    public bool IsAnimating { get { return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"); } }
    ```
    
    <br>
    <br>

    **`해결`**
    
    ![image](https://github.com/user-attachments/assets/2853251a-54c2-48cc-bed5-baed9db1ddd1)

    
<br>
<br>

## ☎️ 팀원 구성 및 연락처 ☎️
### 국기용 (팀장)
- **담당:** `BattleManager`, `BattleScene`, `StageScene`, `EnemyCreator`
- **git 주소:** [github.com/Kaldorei00910](https://github.com/Kaldorei00910)
- **Blog 주소:** [velog.io/@c00kie/posts](https://velog.io/@c00kie/posts)

### 이현장 (부팀장)
- **담당:** `Card`, `CardData`, `UI총괄`, `Store`
- **git 주소:** [github.com/Leehj950](https://github.com/Leehj950)
- **Blog 주소:** [make95.tistory.com](https://make95.tistory.com/)

### 길선호 (팀원)
- **담당:** `BaseCharacter`, `Enemy총괄`, `Rest`
- **git 주소:** [github.com/Kilsunho](https://github.com/Kilsunho)
- **Blog 주소:** [blog.naver.com/sh9567](https://blog.naver.com/sh9567)

### 김영선 (팀원)
- **담당:** `Card Interaction`, `Card Movement`, `Random Map`, `TitleScene`, `SoundManager`
- **git 주소:** [github.com/Mrdosim](https://github.com/Mrdosim)
- **Blog 주소:** [mrdosim.tistory.com](https://mrdosim.tistory.com/)
