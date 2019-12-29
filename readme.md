# MonoInvoker

유니티 함수를 Inspector에서 호출할 수 있도록 도와주는 간단한 툴

해당하는 에셋을 넣고 만들고 싶은 함수뒤에 [TestMethod]만 넣으면 Inspector에서 해당하는 함수를

실행하는 UI를 볼 수 있음


## TestMethodAttribut(bool playable in Editor mode = true)

인자값에 false를 넣으면 PlayMode에서만 나오게 할 수 있음


## Compatible Parameter List

- float
- double
- string
- int
- long
- Color
- Vector2
- Vector3
- Object(Materaial, ScriptableObject, etc..)

## Coroutine(리턴이 IEnumerator)

강제로 playable in editor mode가 false로 되고 플레이중에 누르면 코루틴으로 실행됨
