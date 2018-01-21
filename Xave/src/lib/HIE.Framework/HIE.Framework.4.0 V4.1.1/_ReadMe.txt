
1. 시스템 열거형

	/// <summary>
	/// 정보교류 시스템 에서 사용될 System 유형 입니다.
	/// <para>
	/// 이 열거형은 Solution 에 따라 변경 될 수 있습니다.
	/// </para>
	/// </summary>
	public enum DBEnum
	{
		/// <summary>
		/// 기본 System 입니다.
		/// </summary>
		DEFAULT = 0,
		/// <summary>
		/// ATNA System 입니다.
		/// </summary>
		ATNA = 1,
		/// <summary>
		/// MPI System 입니다.
		/// </summary>
		MPI = 2,
		/// <summary>
		/// Registry System 입니다.
		/// </summary>
		REGISTRY = 3,
		/// <summary>
		/// Repository System 입니다.
		/// </summary>
		REPOSITORY = 4,
		/// <summary>
		/// DSUB System 입니다.
		/// </summary>
		DSUB = 5,
		/// <summary>
		/// CDA System 입니다.
		/// </summary>
		CDA = 6,
		/// <summary>
		/// 성과 모니터링 System 입니다.
		/// </summary>
		RESULT_ANALYSIS = 7,
		/// <summary>
		/// Web Portal System 입니다.
		/// </summary>
		PORTAL = 8,
		/// <summary>
		/// Fhir System 입니다.
		/// </summary>
		FHIR = 9,
		/// <summary>
		/// 정보교류 Log 시스템 입니다.
		/// </summary>
		HIE_LOG = 99,
		/// <summary>
		/// 사용자 정의 System 입니다.
		/// </summary>
		USER_DEFINE = 100,
	}


2. 시스템 사용자 정의 설정

2.1 Domain 설정

	/// <summary>
	/// 데이터 속성과 ORM Mapping Files 를 설정 합니다.
	/// </summary>
	protected override void SetDataSource()
	{
		//base.DataSource = DBEnum.DEFAULT;
		base.DataSource = DBEnum.USER_DEFINE;
		base.DataSourceName = "ZZZ";
	}

2.2 *.Config 파일 의 ORM 설정

	<add name="ZZZ" databaseType="ORACLE" connectionString="Data Source=HIEREGISTRYTEST;User Id=HISEA_PORTAL_USER;Password=test!123;" hbmMappingFilesDir="/DTO/_Mappings/Oracle/" />