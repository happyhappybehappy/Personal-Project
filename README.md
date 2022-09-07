<h1 div align="center">개인 프로젝트-1</div></h1>
<div align="right"> README.md Ver .0.1</div>
<br><br><br>

아이디어노트<br>
- 장르  : (액션) 어드벤처 RPG <br>
- 아이디어  : 와우 모작 / 자연스로운 이동, 물리작용, 상호작용에 중점<br>
- 구현방법<br>
1. 플레이어 이동은 PlayerController, NavMeshAgent 이용 <br>
   --> 키보드를 이용한 PlayerController 이동, 마우스를 이용한 NavMeshAgent 이동<br>
2. 탈 것 구현하여 플레이어의 자식으로 탈 것을 생성하며, 플레이어의 이동속도 값을 수정 /  탈 것 상호작용 시 탈 것에 올라타는 애니메이션 구현<br>
3. 플레이어의 FSM은 IDLE, MOVE, ATTACK, RIDING, INTERACTION, DEAD<br>
4. 몬스터 구현 / 몬스터의 FSM은 IDLE, TRACE, ATTACK, DEAD<br>
5. 상호작용 가능한 오브젝트 구현(상자, NPC, 사다리 등...)<br>
6. 시네머신 카메라를 이용해 백뷰로 구현
<br>
게임 리소스 추출
https://www.inflearn.com/questions/197919
<br><br>


- 만들어야 할 것<br>
    1. 플레이어(전사) 및 NPC<br>
      1.1 플레이어 모델링<br>
      1.2 플레이어 행동(상태패턴/FSM)<br>
        1.2.1 이동 / 점프 (PlayerController, NavMeshAgent)<br>
        1.2.2 공격(Animation)<br>
        1.2.3 상호작용(IInteractable)<br>
    2. 인벤토리 (List<T> / 배열 / Dictionary) 자료구조 결정<br>
    3. 맵<br>
      3.1 맵 모델링 (리소스 추출하기)<br>
      3.2 NavMesh 설정<br>
    4. 소리 - 추출<br>
    5. 탈 것 / 지상탈것, 날탈<br>
      - 탈 것을 플레이어의 자식으로 구현하여 탈 것 소환 시 플레이어가 탈 것에 타는 행동을 취하고 플레이어에 탈 것 스크립트를 넣어 움직임을 따로 구현(상태패턴)<br>
      
   
   1일차 2일차 -> 리소스 추출 및 모델링 건들기<br>
   3일차 4일차 5일차 -> 맵<br>
   6일차 7일차 8일차 -> 플레이어<br>
   8일차 9일차 -> 인벤토리 <br>
   10일차 -> 탈 것<br>
   11일차 -> 소리<br>
   12일차 -> 점검<br>
   13일차 -> ppt 작성<br>
   14일차 -> 발표<br>
     
