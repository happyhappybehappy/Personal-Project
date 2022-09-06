<h1 div align="center">개인 프로젝트-1</div></h1>
<div align="right"> README.md Ver .0.1</div>
<br><br><br>

아이디어노트<br>
- 장르  :  어드벤처 RPG <br>
- 아이디어  : 와우, 원신 모작 / 자연스로운 이동, 상호작용에 중점<br>
- 구현방법<br>
1. 플레이어 이동은 PlayerController, NavMeshAgent 이용 <br>
   --> 키보드를 이용한 PlayerController 이동, 마우스를 이용한 NavMeshAgent 이동<br>
2. 탈 것 구현하여 플레이어의 자식으로 탈 것을 생성하며, 플레이어의 이동속도 값을 수정 /  탈 것 상호작용 시 탈 것에 올라타는 애니메이션 구현<br>
3. 플레이어의 FSM은 IDLE, MOVE, ATTACK, RIDING, INTERACTION, DEAD<br>
4. 몬스터 구현 / 몬스터의 FSM은 IDLE, TRACE, ATTACK, DEAD<br>
5. 상호작용 가능한 오브젝트 구현(상자, NPC, 사다리 등...)
