
1. �ý��� ������

	/// <summary>
	/// �������� �ý��� ���� ���� System ���� �Դϴ�.
	/// <para>
	/// �� �������� Solution �� ���� ���� �� �� �ֽ��ϴ�.
	/// </para>
	/// </summary>
	public enum DBEnum
	{
		/// <summary>
		/// �⺻ System �Դϴ�.
		/// </summary>
		DEFAULT = 0,
		/// <summary>
		/// ATNA System �Դϴ�.
		/// </summary>
		ATNA = 1,
		/// <summary>
		/// MPI System �Դϴ�.
		/// </summary>
		MPI = 2,
		/// <summary>
		/// Registry System �Դϴ�.
		/// </summary>
		REGISTRY = 3,
		/// <summary>
		/// Repository System �Դϴ�.
		/// </summary>
		REPOSITORY = 4,
		/// <summary>
		/// DSUB System �Դϴ�.
		/// </summary>
		DSUB = 5,
		/// <summary>
		/// CDA System �Դϴ�.
		/// </summary>
		CDA = 6,
		/// <summary>
		/// ���� ����͸� System �Դϴ�.
		/// </summary>
		RESULT_ANALYSIS = 7,
		/// <summary>
		/// Web Portal System �Դϴ�.
		/// </summary>
		PORTAL = 8,
		/// <summary>
		/// Fhir System �Դϴ�.
		/// </summary>
		FHIR = 9,
		/// <summary>
		/// �������� Log �ý��� �Դϴ�.
		/// </summary>
		HIE_LOG = 99,
		/// <summary>
		/// ����� ���� System �Դϴ�.
		/// </summary>
		USER_DEFINE = 100,
	}


2. �ý��� ����� ���� ����

2.1 Domain ����

	/// <summary>
	/// ������ �Ӽ��� ORM Mapping Files �� ���� �մϴ�.
	/// </summary>
	protected override void SetDataSource()
	{
		//base.DataSource = DBEnum.DEFAULT;
		base.DataSource = DBEnum.USER_DEFINE;
		base.DataSourceName = "ZZZ";
	}

2.2 *.Config ���� �� ORM ����

	<add name="ZZZ" databaseType="ORACLE" connectionString="Data Source=HIEREGISTRYTEST;User Id=HISEA_PORTAL_USER;Password=test!123;" hbmMappingFilesDir="/DTO/_Mappings/Oracle/" />