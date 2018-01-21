<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:n3="http://www.w3.org/1999/xhtml"
                xmlns:n1="urn:hl7-org:v3"
                xmlns:n2="urn:hl7-org:v3/meta/voc"
                xmlns:voc="urn:hl7-org:v3/voc"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <xsl:output method="html" indent="yes" version="4.01" encoding="ISO-8859-1" doctype-system="http://www.w3.org/TR/html4/strict.dtd" doctype-public="-//W3C//DTD HTML 4.01//EN"/>
  <xsl:param name="limit-external-images" select="'yes'"/>
  <!-- A vertical bar separated list of URI prefixes, such as "http://www.example.com|https://www.example.com" -->
  <xsl:param name="external-image-whitelist"/>
  <!-- string processing variables -->
  <xsl:variable name="lc" select="'abcdefghijklmnopqrstuvwxyz'" />
  <xsl:variable name="uc" select="'ABCDEFGHIJKLMNOPQRSTUVWXYZ'" />
  <!-- removes the following characters, in addition to line breaks "':;?`{}“”„‚’ -->
  <xsl:variable name="simple-sanitizer-match"><xsl:text>&#10;&#13;&#34;&#39;&#58;&#59;&#63;&#96;&#123;&#125;&#8220;&#8221;&#8222;&#8218;&#8217;</xsl:text></xsl:variable>
  <xsl:variable name="simple-sanitizer-replace" select="'***************'"/>
  <xsl:variable name="javascript-injection-warning">WARNING: Javascript injection attempt detected in source CDA document. Terminating</xsl:variable>
  <xsl:variable name="malicious-content-warning">WARNING: Potentially malicious content found in CDA document.</xsl:variable>  
  <!-- CDA document -->
  <xsl:variable name="title">
    <xsl:choose>
      <xsl:when test="string-length(/n1:ClinicalDocument/n1:title)  &gt;= 1">
        <xsl:value-of select="/n1:ClinicalDocument/n1:title"/>
      </xsl:when>
      <xsl:when test="/n1:ClinicalDocument/n1:code/@displayName">
        <xsl:value-of select="/n1:ClinicalDocument/n1:code/@displayName"/>
      </xsl:when>
      <xsl:otherwise>
        <xsl:text>Clinical Document</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:variable>
  <!-- Main -->
  <xsl:template match="/">
    <html xmlns:cda="urn:hl7-org:cda" xmlns:xsi="http://www.w3.org/2000/10/XMLSchema-instance">
      <head>
        <title>
          <xsl:value-of select="$title"/>
        </title>
      </head>
      <body>
        <xsl:apply-templates select="n1:ClinicalDocument"/>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="n1:ClinicalDocument">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>
          <xsl:value-of select="$title"/>
        </title>
        <style type="text/css">
          *{margin:0; padding:0;}
          body {font-family:Dotum; font-size:12px; padding:0; margin:0; border:1px solid #333; border-radius:3px;}
          .body_wrapper{margin:5px auto; width:980px; padding:10px 20px;}

          table{border-collapse:collapse; border-spacing:0; width:100%;}
          table.table_type_1 {border-collapse:collapse; border-spacing:0; width:100%;}
          table.table_type_1 th{font-size:12px; border:1px solid #e1e1e1;}
          table.table_type_1 td{border:1px solid #e1e1e1; padding:6px; text-align:left; color:#666;}

          .table_th{background:#e1e8f0; font-weight:bold; color:#4c7bc3; font-weight:bold;}

          .another_table{font-size:12px; border:2px solid #2a5db6; color:#fff;}
          .another_table td{font-size:12px; padding:0 !important; border:1px solid #bcbcbc !important;}
          .another_table th{font-size:12px; background:#e1e8f0; padding:10px; text-align:left; color:#000;}
          .another_table .tit2_strong{font-size:12px; background:#e1e8f0; padding:10px; text-align:left; color:#2d2e30; display:block;}

          .tit_strong{font-size:12px; color:#000; display:block; font-size:16px !important; margin:20px 0 5px 2px;}

          .odder_table{border-top:3px solid #2a5db8;}
          .odder_table th{background:#e1e8f0; text-align:center; color:#4c7bc3; border:1px solid #bcbcbc !important; font-size:12px !important; border-left:1px solid #838383 !important; border-right:1px solid #838383 !important; padding:10px 0 8px 0 !important; border-bottom:0 !important;}
          .odder_table td{border:1px solid #bcbcbc !important; font-size:12px !important; border-left:1px solid #838383 !important; border-right:1px solid #838383 !important; color:#333 !important; text-align:center !important;}
          .text_td{border:1px solid #838383 !important; padding:15px !important; line-height:1.4 !important;}
          <!--수정 -->
          .h1center { font-size:48px; font-weight:bold; font-weight: bold; text-align: center;}
        </style>
      </head>

      <body topmargin="0" leftmargin="0" marginheight="0" marginwidth="0">
        <div class="xsl-main body_wrapper">
          <h1 class="h1center">
            <xsl:value-of select="$title"/>
          </h1>
          <table border="0" cellpadding="0" cellspacing="0" class="table_type_1">
            <tr>
              <td style="border:0; padding:0;">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                  <!--<tr>
                    <td style="border:0;">
                      <strong style="font-size:48px; font-weight:bold; text-align:center; text-decoration:underline; color:#000; letter-spacing:10px; padding:50px 0; display:block;">
                        <xsl:value-of select="$title"/>
                      </strong>
                      <h1 class="h1center">
                        <xsl:value-of select="$title"/>
                      </h1>
                    </td>
                  </tr>-->
                  <!-- Header -->
                  <!--환자정보 Table-->
									<tr>
										<tr>
                      <td style="border:0; padding:0;">
                        <strong class="tit_strong">환자 정보</strong>
                      </td>
                    </tr>

										<tr>
											<td style="border:0; padding:0;">
												<table border="0" cellpadding="0" cellspacing="0" class="another_table">

													<tr>
														<td width="13%">
															<strong class="tit2_strong">환자명 / 성별</strong>
															<td class="a_td" style="padding:5px !important;" width="20%">
																<!--환자명-->
																<xsl:value-of select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:name/n1:family"/>
																<xsl:value-of select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:name/n1:given"/> /
																<xsl:variable name="sex" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:administrativeGenderCode/@code"/>
																<xsl:if test='$sex="M"'>남</xsl:if>
																<xsl:if test='$sex="F"'>여</xsl:if>
															</td>
														</td>
														<td width="13%">
															<strong class="tit2_strong">연락처</strong>
															<td class="xsl-data a_td"  style="padding:5px !important;" width="20%">
																<xsl:value-of select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:telecom/@value"/>
															</td>
														</td>
														<td width="13%">
															<strong class="tit2_strong">생년월일</strong>
															<td class="xsl-data a_td"  style="padding:5px !important;" width="20%">
																<xsl:call-template name="getDate2">
																	<xsl:with-param name="date" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:birthTime"/>
																</xsl:call-template>
															</td>                            
														</td>
													</tr>													
													<tr>
														<td>
															<strong class="tit2_strong">주소</strong>
															<td colspan="3" class="a_td"  style="padding:5px !important;">
																<xsl:call-template name="getAddr">
																	<xsl:with-param name="addr" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:addr"/>
																</xsl:call-template>
															</td>
														</td>
                            <!-- 진료구분 -->
                            <td width="13%">
                              <strong class="tit2_strong">진료구분</strong>                              
                              <td class="a_td" style="padding:5px !important;" width="20%">                                
                                <xsl:if test="/n1:ClinicalDocument/n1:componentOf/n1:encompassingEncounter/n1:code/@code='AMP'">
                                  <xsl:text>외래</xsl:text>
                                </xsl:if>
                                <xsl:if test="/n1:ClinicalDocument/n1:componentOf/n1:encompassingEncounter/n1:code/@code='IMP'">
                                  <xsl:text>입원</xsl:text>
                                </xsl:if>
                              </td>
                            </td>
													</tr>
												</table>
											</td>
										</tr>
									</tr>
                  <!--기관정보 Table-->
                  <tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <strong class="tit_strong">의료기관 정보</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <table border="0" cellpadding="0" cellspacing="0" class="another_table">
                          <!-- 기관 정보-->
                          <tr>
                            <td width="13%">
                              <strong class="tit2_strong" width="13%">기관명</strong>
                              <td class="a_td" style="padding:5px !important;" width="20%">                                
                                <xsl:value-of select="/n1:ClinicalDocument/n1:custodian/n1:assignedCustodian/n1:representedCustodianOrganization/n1:name"/>                                
                              </td>
                            </td>
                            <td width="13%">
                              <strong class="tit2_strong">기관 전화번호</strong>
                              <td class="a_td"  style="padding:5px !important;" width="20%">
                                <xsl:value-of select="/n1:ClinicalDocument/n1:custodian/n1:assignedCustodian/n1:representedCustodianOrganization/n1:telecom/@value"/>
                              </td>
                            </td>
                            <td width="13%">
                              <strong class="tit2_strong">진료과</strong>
                              <!--<strong class="tit2_strong">요양기관 기호</strong>-->
                              <td class="a_td"  style="padding:5px !important;" width="20%">
                                <xsl:value-of select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:representedOrganization/n1:name"/>
                                <!--<xsl:value-of select="/n1:ClinicalDocument/n1:custodian/n1:assignedCustodian/n1:representedCustodianOrganization/n1:id/@extension"/>-->
                              </td>                              
                            </td>
                          </tr>
                          <tr>
                            <td width="13%">
                              <strong class="tit2_strong" width="13%">주소</strong>
                              <td colspan="5" class="a_td"  style="padding:5px !important;">
                                <xsl:call-template name="getAddr">
                                  <xsl:with-param name="addr" select="/n1:ClinicalDocument/n1:custodian/n1:assignedCustodian/n1:representedCustodianOrganization/n1:addr"/>
                                </xsl:call-template>
                              </td>
                            </td>
                          </tr>
                          <!--의료진 정보-->
                          
                          <tr>
                            <td>
                              <strong class="tit2_strong">의료진 성명</strong>
                              <td class="a_td" style="padding:5px !important;">
                                <xsl:value-of select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedPerson/n1:name/n1:family"/>
                                <xsl:value-of select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedPerson/n1:name/n1:given"/>                                
                              </td>
                            </td>
                            <td>
                              <strong class="tit2_strong">의료진 연락처</strong>
                              <td class="a_td"  style="padding:5px !important;">
                                <xsl:value-of select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:telecom/@value"/>                                
                              </td>
                            </td>
                            <td>
                              <strong class="tit2_strong">의료진 면허번호</strong>
                              <td class="xsl-data a_td"  style="padding:5px !important;">
                                <xsl:value-of select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:id/@extension"/>
                              </td>
                            </td>
                          </tr>
                        <!--<tr>
                            <td>
                              <strong class="tit2_strong">진료과</strong>
                              <td class="a_td" style="padding:10px !important;">
                                <xsl:value-of select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:representedOrganization/n1:name"/>                                
                              </td>
                            </td>
                            <td>
                              <strong class="tit2_strong">작성자</strong>
                              <td class="a_td"  style="padding:5px !important;">
                                <xsl:if test="/n1:ClinicalDocument/n1:participant[@typeCode='AUT'] !=''">
                                  <xsl:value-of select="/n1:ClinicalDocument/n1:participant[@typeCode='AUT']/n1:associatedEntity/n1:associatedPerson/n1:name/n1:family"/>
                                  <xsl:value-of select="/n1:ClinicalDocument/n1:participant[@typeCode='AUT']/n1:associatedEntity/n1:associatedPerson/n1:name/n1:given"/>
                                </xsl:if>                                
                              </td>
                            </td>
                            <td>
                              <strong class="tit2_strong">담당자</strong>
                              <td class="xsl-data a_td"  style="padding:5px !important;">
                                <xsl:if test="/n1:ClinicalDocument/n1:participant[@typeCode='RESP'] !=''">
                                  <xsl:value-of select="/n1:ClinicalDocument/n1:participant[@typeCode='RESP']/n1:associatedEntity/n1:associatedPerson/n1:name/n1:family"/>
                                  <xsl:value-of select="/n1:ClinicalDocument/n1:participant[@typeCode='RESP']/n1:associatedEntity/n1:associatedPerson/n1:name/n1:given"/>
                                </xsl:if>
                              </td>
                            </td>
                          </tr>-->
                          
                        </table>
                      </td>
                    </tr>
                  </tr>                  
                  <!--수신처 정보 Table-->
                  <xsl:if test="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient !=''">
                    <tr>
                      <tr>
                        <td style="border:0; padding:0;">
                          <xsl:choose>
                            <xsl:when test="/n1:ClinicalDocument/n1:code/@code='57133-1'">
                              <strong class="tit_strong">의뢰대상 기관(수신기관) 정보</strong>
                            </xsl:when>
                            <xsl:when test="/n1:ClinicalDocument/n1:code/@code='11488-4'">
                              <strong class="tit_strong">회송대상 기관(수신기관) 정보</strong>
                            </xsl:when>
                            <xsl:otherwise>
                              <strong class="tit_strong">회송대상 기관(수신기관) 정보</strong>
                            </xsl:otherwise>
                          </xsl:choose>
                        </td>
                      </tr>
                      <tr>
                        <td style="border:0; padding:0;">
                          <table border="0" cellpadding="0" cellspacing="0" class="another_table">
                            <tr>
                              <td width="13%">
                                <strong class="tit2_strong" width="13%">수신기관명</strong>
                                <td class="a_td" style="padding:5px !important;" width="20%">
                                  <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:receivedOrganization/n1:asOrganizationPartOf/n1:wholeOrganization/n1:name"/>
                                </td>
                              </td>
                              <td width="13%">
                                <strong class="tit2_strong" width="13%">기관 연락처</strong>
                                <td class="a_td"  style="padding:5px !important;" width="20%">
                                  <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:telecom/@value"/>
                                </td>
                              </td>
                              <td width="13%">
                                <strong class="tit2_strong" width="13%">진료과</strong>
                                <td class="a_td"  style="padding:5px !important;" width="20%">
                                  <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:receivedOrganization/n1:name"/>
                                  <!--<xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:id/@extension"/>-->
                                </td>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <strong class="tit2_strong">주소</strong>
                                <td colspan="3" class="a_td"  style="padding:5px !important;">
                                  <xsl:call-template name="getAddr">
                                    <xsl:with-param name="addr" select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:addr"/>
                                  </xsl:call-template>
                                </td>
                              </td>
                              <td>
                                <strong class="tit2_strong">진료의</strong>
                                <td class="a_td" style="padding:5px !important;">
                                  <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:informationRecipient/n1:name/n1:family"/>
                                  <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:informationRecipient/n1:name/n1:given"/>
                                </td>
                              </td>
                            </tr>
                            <!--의료진 정보-->
                            <!--<tr>
                            <td>
                              <strong class="tit2_strong">의료진 성명</strong>
                              <td class="a_td" style="padding:10px !important;">
                                <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:informationRecipient/n1:name/n1:family"/>
                                <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:informationRecipient/n1:name/n1:given"/>
                              </td>
                            </td>
                            <td>
                              <strong class="tit2_strong">의료진 연락처</strong>
                              <td class="a_td"  style="padding:5px !important;">
                                <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:telecom/@value"/>
                              </td>
                            </td>
                            <td>
                              <strong class="tit2_strong">진료과</strong>
                              <td class="xsl-data a_td"  style="padding:5px !important;">
                                <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:receivedOrganization/n1:name"/>
                              </td>
                            </td>
                          </tr>-->
                          </table>
                        </td>
                      </tr>
                    </tr>
                  </xsl:if>

                  <!-- Body -->
                 
                  <!-- 진단내역 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11450-4'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">진단내역</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <tr>
                          <td style="border:0; padding:0;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td style="border:0; padding:0;">                                  
                                  <xsl:choose>
                                    <!-- 1차 의원용 -->
                                    <xsl:when test="/n1:ClinicalDocument/n1:code/@code='57133-1'">
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="40%">진단일</th>
                                          <th width="60%">진단명</th>                                          
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11450-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td align="center">
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11450-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11450-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>                                                  
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                    </xsl:when>
                                    <!-- 3차 의원용 -->
                                    <xsl:otherwise>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>                                          
                                          <th width="40%">진단일자(최종)</th>                                          
                                          <th width="60%">진단명</th>                                          
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11450-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>                                                  
                                                  <td align="center">
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11450-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>                                                                                                    
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11450-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                      </xsl:otherwise>
                                  </xsl:choose>                                  
                                </td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 투약내역 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">약처방내역</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <tr>
                          <td style="border:0; padding:0;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td style="border:0; padding:0;">
                                  <xsl:choose>
                                    <!-- 1차 의원용 -->
                                    <xsl:when test="/n1:ClinicalDocument/n1:code/@code='57133-1'">
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="20%">약처방일자</th>
                                          <th width="30%">약품명</th>
                                          <th width="10%">용량</th>
                                          <th width="10%">횟수</th>
                                          <th width="10%">일수</th>
                                          <th width="20%">용법</th>
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td >
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td >
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[6]"/>
                                                  </td>
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                    </xsl:when>
                                    <!-- CRS -->
                                    <xsl:when test="/n1:ClinicalDocument/n1:code/@code='34133-9'">
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead><!--30 % 남음-->
                                          <th width="10%">
                                            투여일자<br />(최초)
                                          </th>                                        
                                          <th width="10%">투약명</th>
                                          <th width="10%">용량</th>
                                          <th width="10%">단위</th>
                                          <th width="8%">횟수</th>
                                          <th width="8%">
                                            투여기간<br />(일)
                                          </th>                                          
                                          <th width="20%">주성분명</th>
                                          <th width="24%">용법</th>                                          
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td >
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[6]"/>
                                                  </td>                                                  
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[7]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[8]"/>
                                                  </td>                                                  
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>                                      
                                    </xsl:when>
                                    <!-- 3차 의원용 -->
                                    <xsl:otherwise>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <!--26-->
                                          <th width="8%">투여일자<br />(최초)</th>
                                          <th width="6%">진행상태</th>
                                          <th width="20%">투약명</th>
                                          <th width="5%">용량</th>
                                          <th width="5%">단위</th>
                                          <th width="6%">횟수</th>
                                          <th width="6%">투여기간<br />(일)</th>                                          
                                          <th width="22%">용법</th>
                                          <th width="22%">경로</th>
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td >
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>                                                  
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[6]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[7]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[8]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10160-0']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[9]"/>
                                                  </td>                                                  
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                    </xsl:otherwise>                                    
                                  </xsl:choose>                                  
                                </td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 입원 투약 내역 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='42346-7'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">약처방내역</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <tr>
                          <td style="border:0; padding:0;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td style="border:0; padding:0;">
                                  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                    <thead>
                                      <th width="40%">입원 투약일자</th>                                      
                                      <th width="60%">입원 투약명</th>                                      
                                    </thead>
                                    <tbody>
                                      <tr>
                                        <td style="border:0 !important; padding:0;">
                                          <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='42346-7']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                            <xsl:variable name="position" select="position()"/>
                                            <tr>
                                              <td >
                                                <xsl:call-template name="getDate3">
                                                  <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='42346-7']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                </xsl:call-template>
                                              </td>
                                              <td >
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='42346-7']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                              </td>                                                                                                                                                           
                                            </tr>
                                          </xsl:for-each>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>                                 
                                </td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 검사내역 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">검사내역</strong>                        
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <tr>
                          <td style="border:0; padding:0;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td style="border:0; padding:0;">
                                  <xsl:choose>
                                    <!-- 1차 의원용 진료의뢰서-->
                                    <xsl:when test="/n1:ClinicalDocument/n1:code/@code='57133-1'">
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="15%">검사시행일</th>
                                          <th width="15%">항목명</th>
                                          <th width="20%">검사명</th>
                                          <th width="20%">검사결과</th>
                                          <th width="30%">참고치</th>
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td  >
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td  >
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td  >
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>
                                                  <td >
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                                  </td>
                                                  <td  >
                                                    <!--<xsl:text disable-output-escaping="yes">&lt;![CDATA[</xsl:text>-->
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                                    <!--<xsl:text disable-output-escaping="yes">]]&gt;</xsl:text>-->
                                                  </td>
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                    </xsl:when>
                                    <!-- CRS -->
                                    <xsl:when test="/n1:ClinicalDocument/n1:code/@code='34133-9'">
                                      <strong class="tit_strong">▶검체검사</strong>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="15%">검사일자</th>                                          
                                          <th width="30%">검사명</th>                                          
                                          <th width="35%">검사결과</th>
                                          <th width="20%">참고치</th>
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                                  </td>                                                  
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                      <strong class="tit_strong"></strong>
                                      <!--병리검사-->
                                      <strong class="tit_strong">▶병리검사</strong>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="30%">검사일자</th>
                                          <th width="30%">검사코드(EDI)</th>
                                          <th width="40%">검사명</th>                                          
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>                                                                                                  
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                      <strong class="tit_strong"></strong>
                                      <!--영상검사-->
                                      <strong class="tit_strong">▶영상검사</strong>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="30%">검사일자</th>
                                          <th width="30%">검사코드(EDI)</th>
                                          <th width="40%">검사명</th>                                          
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>                                                  
                                                  <!--<td style="text-align:left !important;">
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                                  </td>-->
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                      <strong class="tit_strong"></strong>
                                      <!--기능검사-->
                                      <strong class="tit_strong">▶기능검사</strong>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="30%">검사일자</th>
                                          <th width="30%">검사코드(EDI)</th>
                                          <th width="40%">검사명</th>                                          
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>                                                                                                    
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                      <strong class="tit_strong"></strong>
                                    </xsl:when>
                                    <!-- 3차 의원용 회송서 / 회신서-->
                                    <xsl:otherwise>
                                      <!--검체검사-->
                                      <strong class="tit_strong">▶검체검사</strong>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="10%">검사처방일</th>
                                          <th width="10%">검사수행일</th>
                                          <th width="15%">최초 결과보고일</th>
                                          <th width="15%">검사명</th>
                                          <th width="20%">검사 항목명</th>
                                          <th width="20%">검사결과</th>
                                          <th width="10%">참고치</th>
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                                  </td>
                                                  <td style="text-align:left !important;">
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[6]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[1]/n1:table/n1:tbody/n1:tr[$position]/n1:td[7]"/>
                                                  </td>
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                      <strong class="tit_strong"></strong>
                                      <!--병리검사-->
                                      <strong class="tit_strong">▶병리검사</strong>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="15%">검사처방일</th>
                                          <th width="15%">검사수행일</th>
                                          <th width="15%">최종 결과보고일</th>
                                          <th width="25%">검사명</th>
                                          <th width="30%">검사결과</th>                                          
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>                                                  
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                                  </td>
                                                  <td style="text-align:left !important;">
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[3]/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                                  </td>                                                  
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                      <strong class="tit_strong"></strong>
                                      <!--영상검사-->
                                      <strong class="tit_strong">▶영상검사</strong>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="15%">검사처방일</th>
                                          <th width="15%">검사수행일</th>
                                          <th width="15%">최종 결과보고일</th>
                                          <th width="25%">검사명</th>
                                          <th width="30%">검사결과</th>
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                                  </td>
                                                  <td style="text-align:left !important;">
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[2]/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                                  </td>
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                      <strong class="tit_strong"></strong>
                                      <!--기능검사-->
                                      <strong class="tit_strong">▶기능검사</strong>
                                      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                        <thead>
                                          <th width="15%">검사처방일</th>
                                          <th width="15%">검사수행일</th>
                                          <th width="15%">최종 결과보고일</th>
                                          <th width="25%">검사명</th>
                                          <th width="30%">검사결과</th>
                                        </thead>
                                        <tbody>
                                          <tr>
                                            <td style="border:0 !important; padding:0;">
                                              <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr">
                                                <xsl:variable name="position" select="position()"/>
                                                <tr>
                                                  <td>
                                                    <xsl:call-template name="getDate3">
                                                      <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                    </xsl:call-template>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                                  </td>
                                                  <td>
                                                    <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='30954-2']/n1:section/n1:text/n1:list/n1:item[4]/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                                  </td>
                                                </tr>
                                              </xsl:for-each>
                                            </td>
                                          </tr>
                                        </tbody>
                                      </table>
                                      <strong class="tit_strong"></strong>
                                      </xsl:otherwise>
                                  </xsl:choose>
                                </td>
                              </tr>
                            </table>
                          </td>
                        </tr>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 수술내역 정보 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">수술내역</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td style="border:0; padding:0;">
                              <xsl:choose>
                                <!--1차 의원용-->
                                <xsl:when test="/n1:ClinicalDocument/n1:code/@code='57133-1'">
                                  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                    <tr>
                                      <thead>
                                        <th width="20%">수술일자</th>
                                        <th width="30%">수술명</th>
                                        <th width="30%">진단명</th>
                                        <th width="20%">마취종류</th>
                                      </thead>
                                    </tr>
                                    <tbody>
                                      <tr>
                                        <td style="border:0 !important; padding:0;">
                                          <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                            <xsl:variable name="position" select="position()"/>
                                            <tr>
                                              <td  >
                                                <xsl:call-template name="getDate3">
                                                  <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                </xsl:call-template>
                                              </td>
                                              <td  >
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                              </td>
                                              <td  >
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                              </td>
                                              <td  >
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                              </td>
                                            </tr>
                                          </xsl:for-each>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </xsl:when>
                                <!--CRS-->
                                <xsl:when test="/n1:ClinicalDocument/n1:code/@code='34133-9'">
                                  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                    <tr>
                                      <thead>15                                        
                                        <th width="20%">수술일자</th>                                        
                                        <th width="40%">수술명</th>
                                        <th width="40%">수술 후 진단명</th>                                        
                                      </thead>
                                    </tr>
                                    <tbody>
                                      <tr>
                                        <td style="border:0 !important; padding:0;">
                                          <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                            <xsl:variable name="position" select="position()"/>
                                            <tr>
                                              <td>
                                                <xsl:call-template name="getDate3">
                                                  <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                </xsl:call-template>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                              </td>                                              
                                            </tr>
                                          </xsl:for-each>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </xsl:when>
                                <!--3차 의원용-->
                                <xsl:otherwise>
                                  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                    <tr>
                                      <thead>
                                        <th width="15%">수술일자</th>
                                        <th width="30%">수술명</th>                                        
                                        <th width="30%">수술 후 진단명</th>
                                        <th width="15%">수술기관</th>
                                        <th width="10%">마취종류</th>
                                      </thead>
                                    </tr>
                                    <tbody>
                                      <tr>
                                        <td style="border:0 !important; padding:0;">
                                          <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                            <xsl:variable name="position" select="position()"/>
                                            <tr>
                                              <td>
                                                <xsl:call-template name="getDate3">
                                                  <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                </xsl:call-template>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='47519-4']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                              </td>                                              
                                            </tr>
                                          </xsl:for-each>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </xsl:otherwise>
                              </xsl:choose>                              
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 예약관련 정보 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">예약 관련내용</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td style="border:0; padding:0;">
                              <xsl:choose>
                                <xsl:when test="/n1:ClinicalDocument/n1:code/@code='11488-4'"> <!--진료회신서-->
                                  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                    <tr>
                                      <thead>
                                        <th width="40%">치료경과</th>
                                        <th width="60%">향후 치료방침</th>
                                      </thead>
                                    </tr>
                                    <tbody>
                                      <tr>
                                        <td style="border:0 !important; padding:0;">
                                          <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                            <xsl:variable name="position" select="position()"/>
                                            <tr>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                              </td>
                                            </tr>
                                          </xsl:for-each>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </xsl:when>
                                <xsl:when test="/n1:ClinicalDocument/n1:code/@code='34133-9'"><!--CRS-->
                                  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                    <tr>
                                      <thead>
                                        <th width="15%">예약희망일시</th>
                                        <th width="40%">예약관련 내용</th>
                                        <th width="15%">병원명</th>
                                        <th width="15%">진료과</th>
                                        <th width="15%">의료진</th>
                                      </thead>
                                    </tr>
                                    <tbody>
                                      <tr>
                                        <td style="border:0 !important; padding:0;">
                                          <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                            <xsl:variable name="position" select="position()"/>
                                            <tr>
                                              <td>
                                                <xsl:call-template name="getDate3">
                                                  <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                                </xsl:call-template>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                              </td>
                                            </tr>
                                          </xsl:for-each>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </xsl:when>
                                <xsl:otherwise><!--진료의뢰서/진료회송서-->                                  
                                  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                    <tr>
                                      <thead>
                                        <th width="40%">예약희망일시</th>
                                        <th width="60%">예약관련 내용</th>
                                      </thead>
                                    </tr>
                                    <tbody>
                                      <tr>
                                        <td style="border:0 !important; padding:0;">
                                          <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                            <xsl:variable name="position" select="position()"/>
                                            <tr>
                                              <td>
                                                <xsl:call-template name="getDate4">
                                                  <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>                                                
                                                </xsl:call-template>                                                
                                              </td>
                                              <td>
                                                <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='18776-5']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                              </td>
                                            </tr>
                                          </xsl:for-each>
                                        </td>
                                      </tr>
                                    </tbody>
                                  </table>
                                </xsl:otherwise>
                              </xsl:choose>                              
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 알러지 정보-->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='48765-2'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">알러지 정보</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td style="border:0; padding:0;">
                              <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                <tr>
                                  <thead>
                                    <th width="15%">등록일자</th>                                    
                                    <th width="35%">알러지명</th>
                                    <th width="20%">반응</th>
                                    <th width="30%">알러지 비고</th>
                                  </thead>
                                </tr>
                                <tbody>
                                  <tr>
                                    <td style="border:0 !important; padding:0;">
                                      <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='48765-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                        <xsl:variable name="position" select="position()"/>
                                        <tr>
                                          <td>
                                            <xsl:call-template name="getDate3">
                                              <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='48765-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                            </xsl:call-template>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='48765-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='48765-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='48765-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='48765-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                          </td>
                                        </tr>
                                      </xsl:for-each>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 예방접종 정보-->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11369-6'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">예방접종 내역</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td style="border:0; padding:0;">
                              <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                <tr>
                                  <thead>20
                                    <th width="25%">접종일자</th>                                    
                                    <th width="25%">백신종류</th>
                                    <th width="30%">백신명</th>
                                    <th width="20%">접종차수</th>
                                  </thead>
                                </tr>
                                <tbody>
                                  <tr>
                                    <td style="border:0 !important; padding:0;">
                                      <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11369-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                        <xsl:variable name="position" select="position()"/>
                                        <tr>
                                          <td>
                                            <xsl:call-template name="getDate3">
                                              <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11369-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                            </xsl:call-template>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11369-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11369-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='11369-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                          </td>                                          
                                        </tr>
                                      </xsl:for-each>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>                              
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 생체정보 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8716-3'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">생체정보</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td style="border:0; padding:0;">
                              <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                <tr>
                                  <thead>
                                    <th width="15%">측정일자</th>
                                    <th width="15%">키</th>
                                    <th width="15%">몸무게</th>
                                    <th width="15%">BMI</th>
                                    <th width="25%">혈압</th>
                                    <th width="15%">체온</th>
                                  </thead>
                                </tr>
                                <tbody>
                                  <tr>
                                    <td style="border:0 !important; padding:0;">
                                      <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8716-3']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                        <xsl:variable name="position" select="position()"/>
                                        <tr>
                                          <td>
                                            <xsl:call-template name="getDate3">
                                              <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8716-3']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                            </xsl:call-template>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8716-3']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8716-3']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8716-3']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8716-3']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8716-3']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[6]"/>
                                          </td>
                                        </tr>
                                      </xsl:for-each>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 방문내역-->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='46240-8'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">방문내역</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td style="border:0; padding:0;">
                              <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                <tr>
                                  <thead>
                                    <th width="20%">방문일자</th>
                                    <th width="30%">병원명</th>
                                    <th width="50%">진단명</th>                                    
                                  </thead>
                                </tr>
                                <tbody>
                                  <tr>
                                    <td style="border:0 !important; padding:0;">
                                      <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='46240-8']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                        <xsl:variable name="position" select="position()"/>
                                        <tr>
                                          <td>
                                            <xsl:call-template name="getDate3">
                                              <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='46240-8']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                            </xsl:call-template>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='46240-8']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='46240-8']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                          </td>                                          
                                        </tr>
                                      </xsl:for-each>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 흡연상태 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='29762-2'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">흡연상태 정보</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td style="border:0; padding:0;">
                              <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                <tr>
                                  <thead>
                                    <th width="30%">상태코드</th>
                                    <th width="70%">상태명</th>                                    
                                  </thead>
                                </tr>
                                <tbody>
                                  <tr>
                                    <td style="border:0 !important; padding:0;">
                                      <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='29762-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                        <xsl:variable name="position" select="position()"/>
                                        <tr>                                          
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='29762-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='29762-2']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                          </td>
                                        </tr>
                                      </xsl:for-each>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 감염성 전염병 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6'] !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">법정 감염성 전염병</strong>
                      </td>
                    </tr>
                    <tr>
                      <td style="border:0; padding:0;">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td style="border:0; padding:0;">
                              <table width="100%" border="0" cellpadding="0" cellspacing="0" class="odder_table">
                                <tr>
                                  <thead>
                                    <th width="10%">발병일자</th>
                                    <th width="10%">진단일</th>
                                    <th width="10%">감염병명</th>
                                    <th width="10%">신고일</th>
                                    <th width="10%">환자분류</th>
                                    <th width="10%">확진검사 결과</th>
                                    <th width="10%">입원여부</th>
                                    <th width="30%">추정감염지역</th>
                                  </thead>
                                </tr>
                                <tbody>
                                  <tr>
                                    <td style="border:0 !important; padding:0;">
                                      <xsl:for-each select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr">
                                        <xsl:variable name="position" select="position()"/>
                                        <tr>
                                          <td>
                                            <xsl:call-template name="getDate3">
                                              <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[1]"/>
                                            </xsl:call-template>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[2]"/>
                                          </td>                                                                                                              
                                          <td>                                                                                                               
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[3]"/>
                                          </td>                                                                                                              
                                          <td>                                                                                                               
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[4]"/>
                                          </td>                                                                                                              
                                          <td>                                                                                                               
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[5]"/>
                                          </td>                                                                                                              
                                          <td>                                                                                                               
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[6]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[7]"/>
                                          </td>
                                          <td>
                                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='56838-6']/n1:section/n1:text/n1:table/n1:tbody/n1:tr[$position]/n1:td[8]"/>
                                          </td>
                                        </tr>
                                      </xsl:for-each>
                                    </td>
                                  </tr>
                                </tbody>
                              </table>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 소견 및 주의사항 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='51848-0']/n1:section/n1:text !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">소견 및 주의사항</strong>
                      </td>
                    </tr>
                    <tr class="xsl-section">
                      <td class="xsl-section-body text_td" style="border:0; padding:0; font-size:12px;">
                        <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='51848-0']/n1:section/n1:text"/>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 의뢰사유 / 회신사유 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='42349-1']/n1:section/n1:text !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <tr>
                          <xsl:choose>
                            <xsl:when test="/n1:ClinicalDocument/n1:code/@code='57133-1'">
                              <td style="border:0; padding:0;">
                                <strong class="tit_strong">의뢰사유</strong>
                                <!--<strong style="font-size: 20px; font-style: italic; font-weight: bold;">의뢰사유</strong>-->
                              </td>
                            </xsl:when>
                            <xsl:when test="/n1:ClinicalDocument/n1:code/@code='11488-4'">
                              <td style="border:0; padding:0;">
                                <strong class="tit_strong">회신사유</strong>
                                <!--<strong style="font-size: 20px; font-style: italic; font-weight: bold;">회신사유</strong>-->
                              </td>
                            </xsl:when>
                            <xsl:when test="/n1:ClinicalDocument/n1:code/@code='18761-7'">
                              <td style="border:0; padding:0;">
                                <strong class="tit_strong">회송사유</strong>
                                <!--<strong style="font-size: 20px; font-style: italic; font-weight: bold;">회신사유</strong>-->
                              </td>
                            </xsl:when>
                          </xsl:choose>
                        </tr>
                        <tr class="xsl-section">
                          <td class="xsl-section-body text_td" style="border:0; padding:0; font-size:12px;">
                            <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='42349-1']/n1:section/n1:text"/>
                          </td>
                        </tr>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 방문사유 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='29299-5']/n1:section/n1:text !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">방문사유</strong>
                      </td>
                    </tr>
                    <tr class="xsl-section">
                      <td class="xsl-section-body text_td" style="border:0; padding:0; font-size:12px;">
                        <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='29299-5']/n1:section/n1:text"/>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 입원사유 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10154-3']/n1:section/n1:text !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">입원사유</strong>
                      </td>
                    </tr>
                    <tr class="xsl-section">
                      <td class="xsl-section-body text_td" style="border:0; padding:0; font-size:12px;">
                        <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='10154-3']/n1:section/n1:text"/>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 퇴원 지시사항 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8653-8']/n1:section/n1:text !=''">
                    <tr>
                      <td style="border:0; padding:10px 0 0 0;">
                        <strong class="tit_strong">퇴원 지시사항</strong>
                      </td>
                    </tr>
                    <tr class="xsl-section">
                      <td class="xsl-section-body text_td" style="border:0; padding:0; font-size:12px;">
                        <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section/n1:code/@code='8653-8']/n1:section/n1:text"/>
                      </td>
                    </tr>
                  </xsl:if>
                  <!-- 서명 이미지 관련 -->
                  <xsl:if test="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component/n1:section/n1:entry/n1:observationMedia/n1:entryRelationship/n1:observation[n1:templateId/@root='2.16.840.1.113883.3.445.19']/n1:value !=''">
                    <table cellspacing="1" cellpadding="1" border="0" bgcolor="#000000">
                      <tr>
                        <td bgcolor="#FFFFFF">
                          <xsl:element name="img">
                            <xsl:attribute name="width">150</xsl:attribute>
                            <xsl:attribute name="height">50</xsl:attribute>
                            <xsl:attribute name="src">
                              data:image/bmp;base64,
                              <xsl:value-of select="/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component/n1:section/n1:entry/n1:observationMedia/n1:entryRelationship/n1:observation[n1:templateId/@root='2.16.840.1.113883.3.445.19']/n1:value"/>
                            </xsl:attribute>
                          </xsl:element>
                        </td>
                      </tr>
                    </table>
                  </xsl:if>
                </table>
              </td>
            </tr>
            <tr>
              <xsl:choose>
                <!--의뢰서-->
                <xsl:when test="/n1:ClinicalDocument/n1:code/@code='57133-1'">
                  <td style="border:0; color:#000; padding:30px 0 5px 0; font-size:15px;">
                    * 이 요양급여의뢰서는 협력기관간 진료의뢰 / 회송 시범사업기관으로 지정 받은 의료기관에서 협력진료체계를 활용하여
                    환자의 상태에 따라 요양급여 의뢰할 때 담당의사가 작성하며, 비용은 진료정보 제공료[의뢰]의 소정점수에 포함됩니다.
                  </td>
                </xsl:when>
                <!--회신서-->
                <xsl:when test="/n1:ClinicalDocument/n1:code/@code='11488-4'">
                  <td style="border:0; color:#000; padding:30px 0 5px 0; font-size:15px;">
                    * 이 요양급여회신서는 협력기관간 진료의뢰 / 회송 시범사업기관으로 지정 받은 의료기관에서 협력진료체계를 활용하여
                    환자의 상태에 따라 요양급여 의뢰할 때 담당의사가 작성하며, 비용은 진료정보 제공료[의뢰]의 소정점수에 포함됩니다.
                  </td>
                </xsl:when><!--회송서-->
                <xsl:when test="/n1:ClinicalDocument/n1:code/@code='18761-7'">
                  <td style="border:0; color:#000; padding:30px 0 5px 0; font-size:15px;">
                    * 이 요양급여회송서는 협력기관간 진료의뢰 / 회송 시범사업기관으로 지정 받은 의료기관에서 협력진료체계를 활용하여
                    환자의 상태에 따라 요양급여 의뢰할 때 담당의사가 작성하며, 비용은 진료정보 제공료[의뢰]의 소정점수에 포함됩니다.
                  </td>
                </xsl:when>
                <xsl:otherwise>                  
              </xsl:otherwise>
              </xsl:choose>             
            </tr>
            <tr>
              <td style="border:0; color:#000; padding:30px 0 5px 0; text-align:right; font-weight:bold; font-size:14px;">
                작성일자 :
                <xsl:call-template name="getDate2">
                  <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:author/n1:time"/>
                </xsl:call-template>
              </td>
            </tr>
          </table>
        </div>
      </body>
    </html>
  </xsl:template>
<!-- recordTarget -->
  
  
  <!-- template-->
  <!-- Get a Date  -->
  <xsl:template name="getDate">
    <xsl:param name="date"/>
    <xsl:choose>
      <xsl:when test="$date/@value">
        <xsl:value-of select="concat(substring($date/@value,1,4),'-',substring($date/@value,5,2),'-',substring($date/@value,7,2),'')"/>
        <xsl:text> </xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$date"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- Get a Date2  -->
  <xsl:template name="getDate2">
    <xsl:param name="date"/>
    <xsl:choose>
      <xsl:when test="$date/@value">
        <xsl:value-of select="concat(substring($date/@value,1,4),'년 ',substring($date/@value,5,2),'월 ',substring($date/@value,7,2),'일')"/>
        <xsl:text> </xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$date"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- Get a Date3  -->
  <xsl:template name="getDate3">
    <xsl:param name="date"/>
    <!--<xsl:variable name="myVar">
      <xsl:call-template name="string-replace-all">
        <xsl:with-param name="text" select="$date"/>
        <xsl:with-param name="replace" select="'-'"/>
        <xsl:with-param name="by" select="''"/>
      </xsl:call-template>
    </xsl:variable>-->
    <xsl:choose>
      <xsl:when test="$date">
        <xsl:if test="$date !=''">
          <xsl:value-of select="concat(substring($date,1,4),'-',substring($date,5,2),'-',substring($date,7,2))"/>
          <!--<xsl:call-template name="getDate">
            <xsl:with-param name="date" select="$date"/>
          </xsl:call-template>-->
        </xsl:if>
        <!--<xsl:with-param name="text" select="concat(substring($date,1,4),'. ',substring($date,5,2),'. ',substring($date,7,2),'')"/>-->
        <xsl:text> </xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="date"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- Get a Date4  -->
  <xsl:template name="getDate4">
    <xsl:param name="date"/>    
    <xsl:choose>
      <xsl:when test="$date">
        <xsl:if test="$date !=''">
          <xsl:value-of select="concat(substring($date,1,4),'-',substring($date,5,2),'-',substring($date,7,2),' ',substring($date,9,2),'시')"/>
          <!--<xsl:call-template name="getDate">
            <xsl:with-param name="date" select="$date"/>
          </xsl:call-template>-->
        </xsl:if>
        <!--<xsl:with-param name="text" select="concat(substring($date,1,4),'. ',substring($date,5,2),'. ',substring($date,7,2),'')"/>-->
        <xsl:text> </xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="date"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template name="string-replace-all">
    <xsl:param name="text" />
    <xsl:param name="replace" />
    <xsl:param name="by" />
    <xsl:choose>
      <xsl:when test="contains($text, $replace)">
        <xsl:value-of select="substring-before($text,$replace)" />
        <xsl:value-of select="$by" />
        <xsl:call-template name="string-replace-all">
          <xsl:with-param name="text"
          select="substring-after($text,$replace)" />
          <xsl:with-param name="replace" select="$replace" />
          <xsl:with-param name="by" select="$by" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$text" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- Get a addr  -->
  <xsl:template name="getAddr">
    <xsl:param name="addr"/>
    <xsl:value-of select="$addr/n1:state"/>
        <xsl:text>  </xsl:text>
        <xsl:value-of select="$addr/n1:city"/>
        <xsl:text>  </xsl:text>
        <xsl:value-of select="$addr/n1:additionalLocator"/>
        <xsl:text>   </xsl:text>
        <xsl:value-of select="$addr/n1:streetAddressLine"/>
        <xsl:text>   </xsl:text>        
        <xsl:if test="$addr/n1:postalCode !=''">
          <xsl:text> / </xsl:text>
        </xsl:if>
        <xsl:value-of select="$addr/n1:postalCode"/>
    <!--<xsl:choose>
      <xsl:when test="$addr/n1:country">
        --><!--국가명 생략주석 : 황성호--><!--
        --><!--<xsl:value-of select="$addr/n1:country"/>
        <xsl:text>  </xsl:text>--><!--
        
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$addr"/>
      </xsl:otherwise>
    </xsl:choose>-->
  </xsl:template>

  <!--   Title  -->
  <xsl:template match="n1:title">
    <table style="WIDTH: 100%;">
      <tr style="WIDTH: 100%;">
        <td height="30" class="bullet" style="padding-left:10pt ">
          <strong style="font-size: 14px; margin-left:5px; color: #00C;">
            <xsl:value-of select="."></xsl:value-of>
          </strong>
        </td>
      </tr>
      <tr>
        <td background="./titleLine.png" height="2"></td>
      </tr>
    </table>
  </xsl:template>

  <!--   Text   -->
  <xsl:template match="n1:text">
    <span height="11" style="FONT-SIZE: 9pt; padding-left:10pt;">
      <xsl:apply-templates />
    </span>
  </xsl:template>

  <!--   paragraph  -->
  <xsl:template match="n1:paragraph">
    <xsl:apply-templates/>
  </xsl:template>

  <!--     Content w/ deleted text is hidden -->
  <xsl:template match="n1:content[@revised='delete']"/>

  <!--   content  -->
  <xsl:template match="n1:content">
    <xsl:apply-templates/>
  </xsl:template>

  <!--   list  -->
  <xsl:template match="n1:list">
    <xsl:if test="n1:caption">
      <span style="font-weight:bold; ">
        <xsl:apply-templates select="n1:caption"/>
      </span>
    </xsl:if>
    <table width="95%">
      <xsl:for-each select="n1:item">
        <tr width="100%">
          <td align="justify" style="font-size: 9pt; padding-left:10pt; line-height:14pt">
            <xsl:apply-templates />
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

  <xsl:template match="n1:table">
    <table  width="100%" border="1" bordercolor="1076bd" style="border-collapse:collapse; border-style:solid;" cellpadding="3" cellspacing="0">
      <xsl:apply-templates/>
    </table>
  </xsl:template>

  <xsl:template match="n1:thead">
    <thead>
      <xsl:apply-templates/>
    </thead>
  </xsl:template>

  <xsl:template match="n1:tbody">
    <tbody>
      <xsl:apply-templates/>
    </tbody>
  </xsl:template>

  <xsl:template match="n1:th">
    <th width="17%">
      <strong style="color:#FFF">
        <xsl:apply-templates/>
      </strong>
    </th>
  </xsl:template>
  <xsl:template match="n1:tr">
    <tr style="font-size:14px">
      <xsl:apply-templates/>
    </tr>
  </xsl:template>

  <xsl:template match="n1:td">
    <td>
      <xsl:apply-templates/>
      <font color="#ffffc3" >.</font>
    </td>
  </xsl:template>

</xsl:stylesheet>



