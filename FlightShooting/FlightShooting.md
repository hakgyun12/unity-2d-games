유니티에서 제공하는 GameObject 클래스의 Find(), FindObjectOfType(), FindGameObjectWithTag()와 같은 함수들은 Hierarchy View에 있는 게임 오브젝트 정보를 불러올 때 사용하는 함수들로 퍼포먼스가 굉장히 나쁘기 때문에 사용을 지양해야 한다. 특히 Find(), FindObjectOfType()은 절대 사용해선 안될 정도로 퍼포먼스가 좋지않다.

필요한 경우에는 미리 찾아주도록 Awake()와 같은 함수내에서 FindGameObjectWithTag()를 사용해 오브젝트 정보를 변수에 저장해두고 변수를 클래스 내에서 사용하는 것이 좋다.