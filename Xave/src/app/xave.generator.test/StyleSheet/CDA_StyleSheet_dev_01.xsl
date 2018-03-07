<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:n1="urn:hl7-org:v3"
                xmlns:in="urn:inline-variable-data">

  <xsl:output method="html" indent="yes" version="4.01" encoding="ISO-8859-1" doctype-system="http://www.w3.org/TR/html4/strict.dtd" doctype-public="-//W3C//DTD HTML 4.01//EN"/>
  <xsl:param name="limit-external-images" select="'yes'"/>  
  
  <xsl:variable name="totalCount" select="count(/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section])"/>

  <!-- document title -->
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
    <xsl:apply-templates select="n1:ClinicalDocument"/>
  </xsl:template>
  <xsl:template match="n1:ClinicalDocument">
    <html xml:lang="ko">
      <head>
        <meta http-equiv='X-UA-Compatible' content='IE=8'/>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="Content-Script-Type" content="text/javascript" />
        <meta http-equiv="Content-Style-Type" content="text/css" />
        <title>
          <xsl:value-of select="$title"/>
        </title>
        <xsl:call-template name="addCSS"/>
      </head>

      <body onLoad="MM_preloadImages('images/icon_patient_on.png')">
        <div id="wrap">
          <table width="660" border="0" align="center" cellpadding="0" cellspacing="0">
            
          </table>
        </div>
        

        
        <script type="text/javascript">
          <xsl:comment>
            <![CDATA[          
        function Expend(spn) {
            var img = spn.firstChild;
            var div = spn.parentNode.parentNode.lastChild;

            if (div.style.display == "none") {
              div.style.display = "";
            
              img.src = "data:image/gif;base64,R0lGODlhDQAIAIABAGZmZv///yH/C1hNUCBEYXRhWE1QPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS4zLWMwMTEgNjYuMTQ1NjYxLCAyMDEyLzAyLzA2LTE0OjU2OjI3ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdFJlZj0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlUmVmIyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M2IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpCQjRBQUMxQzM3NTUxMUU2OTcxNUQ5MkMyMDQwMjgxMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpCQjRBQUMxRDM3NTUxMUU2OTcxNUQ5MkMyMDQwMjgxMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkJCNEFBQzFBMzc1NTExRTY5NzE1RDkyQzIwNDAyODExIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkJCNEFBQzFCMzc1NTExRTY5NzE1RDkyQzIwNDAyODExIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+Af/+/fz7+vn49/b19PPy8fDv7u3s6+rp6Ofm5eTj4uHg397d3Nva2djX1tXU09LR0M/OzczLysnIx8bFxMPCwcC/vr28u7q5uLe2tbSzsrGwr66trKuqqainpqWko6KhoJ+enZybmpmYl5aVlJOSkZCPjo2Mi4qJiIeGhYSDgoGAf359fHt6eXh3dnV0c3JxcG9ubWxramloZ2ZlZGNiYWBfXl1cW1pZWFdWVVRTUlFQT05NTEtKSUhHRkVEQ0JBQD8+PTw7Ojk4NzY1NDMyMTAvLi0sKyopKCcmJSQjIiEgHx4dHBsaGRgXFhUUExIREA8ODQwLCgkIBwYFBAMCAQAAIfkEAQAAAQAsAAAAAA0ACAAAAhIEgpnGuhaakhEieiF+e1o3gQUAOw==";
            }
            else {
              div.style.display = "none";
              img.src = "data:image/gif;base64,R0lGODlhDQAIAIABAGZmZv///yH/C1hNUCBEYXRhWE1QPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS4zLWMwMTEgNjYuMTQ1NjYxLCAyMDEyLzAyLzA2LTE0OjU2OjI3ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdFJlZj0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlUmVmIyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M2IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpFNUM2NDM2QTM3NTUxMUU2QjQ5MUVBMkYxN0E2MzQ2OSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpFNUM2NDM2QjM3NTUxMUU2QjQ5MUVBMkYxN0E2MzQ2OSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkU1QzY0MzY4Mzc1NTExRTZCNDkxRUEyRjE3QTYzNDY5IiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkU1QzY0MzY5Mzc1NTExRTZCNDkxRUEyRjE3QTYzNDY5Ii8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+Af/+/fz7+vn49/b19PPy8fDv7u3s6+rp6Ofm5eTj4uHg397d3Nva2djX1tXU09LR0M/OzczLysnIx8bFxMPCwcC/vr28u7q5uLe2tbSzsrGwr66trKuqqainpqWko6KhoJ+enZybmpmYl5aVlJOSkZCPjo2Mi4qJiIeGhYSDgoGAf359fHt6eXh3dnV0c3JxcG9ubWxramloZ2ZlZGNiYWBfXl1cW1pZWFdWVVRTUlFQT05NTEtKSUhHRkVEQ0JBQD8+PTw7Ojk4NzY1NDMyMTAvLi0sKyopKCcmJSQjIiEgHx4dHBsaGRgXFhUUExIREA8ODQwLCgkIBwYFBAMCAQAAIfkEAQAAAQAsAAAAAA0ACAAAAhOMD6cKvdzci4HCZGyVyOnLTUABADs=";
            }
        
        }

        function popupView(page)
        {
            try{
                window.open(page, "", "width=600px,height=500px,alwaysRaised=yes,resizable=yes,center=yes,status=no");
            }
            catch(exception){
            }
        }
        
        function MM_swapImgRestore() { //v3.0
            var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
        }
        
        function MM_preloadImages() { //v3.0
            var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
            var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
            if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
        }

        function MM_findObj(n, d) { //v4.01
            var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
            d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
            if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
            for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
            if(!x && d.getElementById) x=d.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
            var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
            if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
        }
    ]]>
          </xsl:comment>
        </script>
      </body>
    </html>
  </xsl:template>
  <!-- CSS -->
  <xsl:template name="addCSS">
    <style type="text/css">
      <xsl:text>      
			html,body{width:100%;height:100%}
			html{overflow-y:scroll}
			body,div,dl,dt,dd,ul,ol,li,h1,h2,h3,h4,h5,form,fieldset,p,button,tr,td{margin:0;padding:0;}
			body,h1,h2,h3,h4,input,button{font-family: 나눔바른고딕, nanumBarunGodic,verdana,Helvetica,sans-serif;font-size:12px;color:#1a1a1a}
			body{background-color:#fff;*word-break:break-all;-ms-word-break:break-all}
			img,fieldset,iframe{border:0 none}
			li{	list-style: none; font-size: 12px; }

.text_white{font-size: 12px; color: #ffffff; padding:0px 10px;	}
			
.text_white_b{font-size: 12px; color: #ffffff; font-weight:600; }		

.text_white17 {font-size: 17px; color: #ffffff; margin: 0px 0px 0px 5px }	
			
.text_white18_b {font-size: 18px; color: #ffffff; font-weight:600; margin: 0px 0px 0px 10px	}					
			
.text_white{font-size: 12px; color: #ffffff; }		

.text_date{font-size: 12px; color: #999999;}				
				
.text_gray1_13{font-size: 13px;  color: #1a1a1a; }
			
.text_gray1_13_b {font-size: 13px;  color: #1a1a1a; font-weight:600; }			
			
.text_gray2_13{	font-size: 13px; color: #4c4c4c;}		
			
.text_gray3_13{font-size: 13px; color: #808080; }
			
.text_gray4_13{font-size: 13px; color: #999999; padding-left:10px; }	
			
.text_gray4_15_b {font-size: 15px; color: #999999; font-weight:600;}	
			
.text_gray5_13{font-size: 13px; color: #cccccc; }	

.text_info1{font-size: 15px; color: #1a1a1a; padding-right: 10px; line-height:1.8em; }

.text_highlighter_yellow{background:#ffff7e	}
			
.text_active_orange {font-size: 13px; font-weight:600; color: #e04138; padding: 0px 10px 0px 20px;}			
			
.text_inactive_gray {font-size: 13px; font-weight:600; color: #4d4d4d; padding: 0px 20px 0px 10px;}	
			
.text_border_orange {color:#ffffff ; font-size:12px;  border-radius:2px; background:#d14d15; margin: 8px; padding:6px 12px;}

.text_box_gray  {color:#1a1a1a ; font-size:12px; border:1px #e6e6e6 solid ; background:#fafafa;  border-radius:5px; margin:5px;  padding:5px;}	
	
.date_line {height:100%;}	
						
#wrap{width:684px;margin:0 auto; background:#fc7945}	 
	   
#wrap_gray{width:684px;margin:0 auto; background:#ecedf1}	   
	     

a { color: #4c4c4c;
	font-family: 나눔바른고딕, nanumBarunGodic,verdana,Helvetica,sans-serif;
	font-size:15px;
	font-weight:600;
	padding:10px 10px 0 10px;
	text-decoration:none;
}

a:hover {
	font-family: 나눔바른고딕, nanumBarunGodic,verdana,He,sans-serif;
	color:#ea7f52;
	border-bottom:3px solid #ea7f52;
	padding:0 10px 11px 10px;
}


a:visited {
	font-color:#4c4c4c;
	border-bottom:3px solid #ea7f52;

}

.tab_on {
	font-family: 나눔바른고딕, nanumBarunGodic,verdana,Helvetica,sans-serif;
	font-size:15px;
	font-weight:600;
	text-decoration:none;
	color:#ea7f52;
	border-bottom:3px solid #ea7f52;
	padding:0 10px 11px 10px;
}

    </xsl:text>
    </style>
  </xsl:template>
  <!-- show nonXMLBody -->
  
  <!-- show-signature -->
  <!-- show-id -->
  <!-- show-name -->
  <!-- show-gender -->
  <!-- show-race-ethnicity -->
  <!-- show-contactInfo -->
  <!-- show-address -->
  <!-- show-telecom -->
  <!-- show-recipientType -->
  <!-- Convert Telecom URL to display text -->
  <xsl:template name="translateTelecomCode">
    <xsl:param name="code"/>
    <xsl:choose>
      <!-- lookup table Telecom URI -->
      <xsl:when test="$code='tel'">
        <xsl:text>Tel</xsl:text>
      </xsl:when>
      <xsl:when test="$code='fax'">
        <xsl:text>Fax</xsl:text>
      </xsl:when>
      <xsl:when test="$code='http'">
        <xsl:text>Web</xsl:text>
      </xsl:when>
      <xsl:when test="$code='mailto'">
        <xsl:text>Mail</xsl:text>
      </xsl:when>
      <xsl:when test="$code='H'">
        <xsl:text>Home</xsl:text>
      </xsl:when>
      <xsl:when test="$code='HV'">
        <xsl:text>Vacation Home</xsl:text>
      </xsl:when>
      <xsl:when test="$code='HP'">
        <xsl:text>Primary Home</xsl:text>
      </xsl:when>
      <xsl:when test="$code='WP'">
        <xsl:text>Work Place</xsl:text>
      </xsl:when>
      <xsl:when test="$code='PUB'">
        <xsl:text>Pub</xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:text>{$code='</xsl:text>
        <xsl:value-of select="$code"/>
        <xsl:text>'?}</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- show time -->
  <!-- paticipant facility and date -->
  <!-- show assignedEntity -->
  <!-- show relatedEntity -->
  <!-- show associatedEntity -->
  <!-- show code if originalText present, return it, otherwise, check and return attribute: display name -->
  <xsl:template name="show-code">
    <xsl:param name="code"/>
    <xsl:variable name="this-codeSystem">
      <xsl:value-of select="$code/@codeSystem"/>
    </xsl:variable>
    <xsl:variable name="this-code">
      <xsl:value-of select="$code/@code"/>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="$code/n1:originalText">
        <xsl:value-of select="$code/n1:originalText"/>
      </xsl:when>
      <xsl:when test="$code/@displayName">
        <xsl:value-of select="$code/@displayName"/>
      </xsl:when>
      <!--
         <xsl:when test="$the-valuesets/*/voc:system[@root=$this-codeSystem]/voc:code[@value=$this-code]/@displayName">
           <xsl:value-of select="$the-valuesets/*/voc:system[@root=$this-codeSystem]/voc:code[@value=$this-code]/@displayName"/>
         </xsl:when>
         -->
      <xsl:otherwise>
        <xsl:value-of select="$this-code"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- show classCode -->
  <!-- show participationType -->
  <!-- show participationFunction -->
  <!-- convert to lower case -->
  <xsl:template name="caseDown">
    <xsl:param name="data"/>
    <xsl:if test="$data">
      <xsl:value-of select="translate($data, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')"/>
    </xsl:if>
  </xsl:template>
  <!-- convert to upper case -->
  <xsl:template name="caseUp">
    <xsl:param name="data"/>
    <xsl:if test="$data">
      <xsl:value-of select="translate($data,'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')"/>
    </xsl:if>
  </xsl:template>
  <!-- convert first character to upper case -->
  <xsl:template name="firstCharCaseUp">
    <xsl:param name="data"/>
    <xsl:if test="$data">
      <xsl:call-template name="caseUp">
        <xsl:with-param name="data" select="substring($data,1,1)"/>
      </xsl:call-template>
      <xsl:value-of select="substring($data,2)"/>
    </xsl:if>
  </xsl:template>
  <!-- show-noneFlavor -->
  <xsl:template name="show-noneFlavor">
    <xsl:param name="nf"/>
    <xsl:choose>
      <xsl:when test=" $nf = 'NI' ">
        <xsl:text>no information</xsl:text>
      </xsl:when>
      <xsl:when test=" $nf = 'INV' ">
        <xsl:text>invalid</xsl:text>
      </xsl:when>
      <xsl:when test=" $nf = 'MSK' ">
        <xsl:text>masked</xsl:text>
      </xsl:when>
      <xsl:when test=" $nf = 'NA' ">
        <xsl:text>not applicable</xsl:text>
      </xsl:when>
      <xsl:when test=" $nf = 'UNK' ">
        <xsl:text>unknown</xsl:text>
      </xsl:when>
      <xsl:when test=" $nf = 'OTH' ">
        <xsl:text>other</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <!-- ########################################################### -->
  <!-- ##################### Narrative Block ##################### -->
  <!-- ########################################################### -->
  <!-- show StructuredBody  -->
  <xsl:template match="n1:component/n1:structuredBody">
    <xsl:for-each select="n1:component/n1:section">
      <xsl:call-template name="section"/>
    </xsl:for-each>
  </xsl:template>
  <!-- Section -->
  <xsl:template name="section">
    <div class="main_content">
      <h2 class="content_title1 cnt">
        <xsl:call-template name="section-title">
          <xsl:with-param name="title" select="n1:title"/>
        </xsl:call-template>
        <span class="expender" onclick="javascript: Expend(this);">
          <img src="data:image/gif;base64,R0lGODlhDQAIAIABAGZmZv///yH/C1hNUCBEYXRhWE1QPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS4zLWMwMTEgNjYuMTQ1NjYxLCAyMDEyLzAyLzA2LTE0OjU2OjI3ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdFJlZj0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlUmVmIyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M2IChXaW5kb3dzKSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpCQjRBQUMxQzM3NTUxMUU2OTcxNUQ5MkMyMDQwMjgxMSIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDpCQjRBQUMxRDM3NTUxMUU2OTcxNUQ5MkMyMDQwMjgxMSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkJCNEFBQzFBMzc1NTExRTY5NzE1RDkyQzIwNDAyODExIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOkJCNEFBQzFCMzc1NTExRTY5NzE1RDkyQzIwNDAyODExIi8+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+Af/+/fz7+vn49/b19PPy8fDv7u3s6+rp6Ofm5eTj4uHg397d3Nva2djX1tXU09LR0M/OzczLysnIx8bFxMPCwcC/vr28u7q5uLe2tbSzsrGwr66trKuqqainpqWko6KhoJ+enZybmpmYl5aVlJOSkZCPjo2Mi4qJiIeGhYSDgoGAf359fHt6eXh3dnV0c3JxcG9ubWxramloZ2ZlZGNiYWBfXl1cW1pZWFdWVVRTUlFQT05NTEtKSUhHRkVEQ0JBQD8+PTw7Ojk4NzY1NDMyMTAvLi0sKyopKCcmJSQjIiEgHx4dHBsaGRgXFhUUExIREA8ODQwLCgkIBwYFBAMCAQAAIfkEAQAAAQAsAAAAAA0ACAAAAhIEgpnGuhaakhEieiF+e1o3gQUAOw==" alt="" />
        </span>
      </h2>
      <xsl:call-template name="section-text"/>
      <xsl:for-each select="n1:component/n1:section">
        <xsl:call-template name="nestedSection">
          <xsl:with-param name="margin" select="2"/>
        </xsl:call-template>
      </xsl:for-each>
    </div>
  </xsl:template>
  <!-- section title-->
  <xsl:template name="section-title">
    <xsl:param name="title"/>
    <xsl:value-of select="$title"/>
  </xsl:template>
  <!-- section title-link-->
  <xsl:template name="section-title-Link">
    <xsl:param name="title"/>
    <xsl:choose>
      <xsl:when test="count(/n1:ClinicalDocument/n1:component/n1:structuredBody/n1:component[n1:section]) &gt; 1">
        <a name="{generate-id($title)}" href="#toc" style="color:#222;">
          <xsl:value-of select="$title"/>
        </a>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$title"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- section author -->
  <!-- top-level section Text   -->
  <xsl:template name="section-text">
    <div>
      <xsl:apply-templates select="n1:text"/>
    </div>
    <!--<div class ="main_content">
      <div>
        <xsl:apply-templates select="n1:text"/>
      </div>
    </div>-->
  </xsl:template>
  <!-- nested component/section -->
  <xsl:template name="nestedSection">
    <xsl:param name="margin"/>
    <h4 style="margin-left : {$margin}em;">
      <xsl:value-of select="n1:title"/>
    </h4>
    <div style="margin-left : {$margin}em;">
      <xsl:apply-templates select="n1:text"/>
    </div>
    <xsl:for-each select="n1:component/n1:section">
      <xsl:call-template name="nestedSection">
        <xsl:with-param name="margin" select="2*$margin"/>
      </xsl:call-template>
    </xsl:for-each>
  </xsl:template>
  <!--   paragraph  -->
  <xsl:template match="n1:paragraph">
    <p>
      <xsl:apply-templates/>
    </p>
  </xsl:template>
  <!--   pre format  -->
  <xsl:template match="n1:pre">
    <pre>
      <xsl:apply-templates/>
    </pre>
  </xsl:template>
  <!--   Content w/ deleted text is hidden -->
  <xsl:template match="n1:content[@revised='delete']"/>
  <!--   content  -->
  <xsl:template match="n1:content">
    <span>
      <xsl:apply-templates select="@styleCode"/>
      <xsl:apply-templates/>
    </span>
  </xsl:template>
  <!-- line break -->
  <xsl:template match="n1:br">
    <xsl:element name='br'>
      <xsl:apply-templates/>
    </xsl:element>
  </xsl:template>
  <!--   list  -->
  <xsl:template match="n1:list">
    <xsl:if test="n1:caption">
      <p>
        <strong>
          <xsl:apply-templates select="n1:caption"/>
        </strong>
      </p>
    </xsl:if>
    <ul>
      <xsl:for-each select="n1:item">
        <li>
          <xsl:apply-templates/>
        </li>
      </xsl:for-each>
    </ul>
  </xsl:template>
  <xsl:template match="n1:list[@listType='ordered']">
    <xsl:if test="n1:caption">
      <span style="font-weight:bold; ">
        <xsl:apply-templates select="n1:caption"/>
      </span>
    </xsl:if>
    <ol>
      <xsl:for-each select="n1:item">
        <li>
          <xsl:apply-templates/>
        </li>
      </xsl:for-each>
    </ol>
  </xsl:template>
  <!--   caption  -->
  <xsl:template match="n1:caption">
    <xsl:apply-templates/>
    <xsl:text>: </xsl:text>
  </xsl:template>
  <!--  Tables   -->
  <xsl:variable name="table-elem-attrs">
    <in:tableElems>
      <in:elem name="table">
        <in:attr name="ID"/>
        <in:attr name="language"/>
        <in:attr name="styleCode"/>
        <in:attr name="summary"/>
        <in:attr name="width"/>
        <in:attr name="border"/>
        <in:attr name="frame"/>
        <in:attr name="rules"/>
        <in:attr name="cellspacing"/>
        <in:attr name="cellpadding"/>
      </in:elem>
      <in:elem name="thead">
        <in:attr name="ID"/>
        <in:attr name="language"/>
        <in:attr name="styleCode"/>
        <in:attr name="align"/>
        <in:attr name="char"/>
        <in:attr name="charoff"/>
        <in:attr name="valign"/>
      </in:elem>
      <in:elem name="tfoot">
        <in:attr name="ID"/>
        <in:attr name="language"/>
        <in:attr name="styleCode"/>
        <in:attr name="align"/>
        <in:attr name="char"/>
        <in:attr name="charoff"/>
        <in:attr name="valign"/>
      </in:elem>
      <in:elem name="tbody">
        <in:attr name="ID"/>
        <in:attr name="language"/>
        <in:attr name="styleCode"/>
        <in:attr name="align"/>
        <in:attr name="char"/>
        <in:attr name="charoff"/>
        <in:attr name="valign"/>
      </in:elem>
      <in:elem name="colgroup">
        <in:attr name="ID"/>
        <in:attr name="language"/>
        <in:attr name="styleCode"/>
        <in:attr name="span"/>
        <in:attr name="width"/>
        <in:attr name="align"/>
        <in:attr name="char"/>
        <in:attr name="charoff"/>
        <in:attr name="valign"/>
      </in:elem>
      <in:elem name="col">
        <in:attr name="ID"/>
        <in:attr name="language"/>
        <in:attr name="styleCode"/>
        <in:attr name="span"/>
        <in:attr name="width"/>
        <in:attr name="align"/>
        <in:attr name="char"/>
        <in:attr name="charoff"/>
        <in:attr name="valign"/>
      </in:elem>
      <in:elem name="tr">
        <in:attr name="ID"/>
        <in:attr name="language"/>
        <in:attr name="styleCode"/>
        <in:attr name="align"/>
        <in:attr name="char"/>
        <in:attr name="charoff"/>
        <in:attr name="valign"/>
      </in:elem>
      <in:elem name="th">
        <in:attr name="ID"/>
        <in:attr name="language"/>
        <in:attr name="styleCode"/>
        <in:attr name="abbr"/>
        <in:attr name="axis"/>
        <in:attr name="headers"/>
        <in:attr name="scope"/>
        <in:attr name="rowspan"/>
        <in:attr name="colspan"/>
        <in:attr name="align"/>
        <in:attr name="char"/>
        <in:attr name="charoff"/>
        <in:attr name="valign"/>
      </in:elem>
      <in:elem name="td">
        <in:attr name="ID"/>
        <in:attr name="language"/>
        <in:attr name="styleCode"/>
        <in:attr name="abbr"/>
        <in:attr name="axis"/>
        <in:attr name="headers"/>
        <in:attr name="scope"/>
        <in:attr name="rowspan"/>
        <in:attr name="colspan"/>
        <in:attr name="align"/>
        <in:attr name="char"/>
        <in:attr name="charoff"/>
        <in:attr name="valign"/>
      </in:elem>
    </in:tableElems>
  </xsl:variable>
  <xsl:template name="output-attrs">
    <xsl:variable name="elem-name" select="local-name(.)"/>
    <xsl:for-each select="@*">
      <xsl:variable name="attr-name" select="local-name(.)"/>
      <xsl:variable name="source" select="."/>      
      <xsl:choose>        
        <xsl:when test="$attr-name='styleCode'">
          <xsl:apply-templates select="."/>
        </xsl:when>
        <xsl:when test="not(document('')/xsl:stylesheet/xsl:variable[@name='table-elem-attrs']/in:tableElems/in:elem[@name=$elem-name]/in:attr[@name=$attr-name])">
          <xsl:message>
            <xsl:value-of select="$attr-name"/> is not legal in <xsl:value-of select="$elem-name"/>
          </xsl:message>
        </xsl:when>        
        <xsl:otherwise>
          <xsl:copy-of select="."/>
        </xsl:otherwise>
      </xsl:choose>
    </xsl:for-each>
  </xsl:template>

  <!-- 16. 06 .22 추가-->
  <xsl:template match="n1:table">
    <xsl:element name="{local-name()}">
      <xsl:attribute name="class">contents_table1</xsl:attribute>
      <xsl:call-template name="output-attrs"/>
      <xsl:apply-templates/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="n1:thead | n1:tfoot | n1:tbody | n1:colgroup | n1:col | n1:tr | n1:th | n1:td">
    <xsl:element name="{local-name()}">
      <xsl:call-template name="output-attrs"/>
      <xsl:apply-templates/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="n1:table/n1:caption">
    <span style="font-weight:bold; ">
      <xsl:apply-templates/>
    </span>
  </xsl:template>
  <!--   RenderMultiMedia  -->
  <!--   Stylecode  -->
  <xsl:template match="//n1:*[@styleCode]">
    <xsl:if test="@styleCode='Bold'">
      <b>
        <xsl:apply-templates/>
      </b>
    </xsl:if>
    <xsl:if test="@styleCode='Italics'">
      <xsl:element name="i">
        <xsl:apply-templates/>
      </xsl:element>
    </xsl:if>
    <xsl:if test="@styleCode='Underline'">
      <xsl:element name="u">
        <xsl:apply-templates/>
      </xsl:element>
    </xsl:if>
    <xsl:if test="contains(@styleCode,'Bold') and contains(@styleCode,'Italics') and not (contains(@styleCode, 'Underline'))">
      <xsl:element name="b">
        <xsl:element name="i">
          <xsl:apply-templates/>
        </xsl:element>
      </xsl:element>
    </xsl:if>
    <xsl:if test="contains(@styleCode,'Bold') and contains(@styleCode,'Underline') and not (contains(@styleCode, 'Italics'))">
      <xsl:element name="b">
        <xsl:element name="u">
          <xsl:apply-templates/>
        </xsl:element>
      </xsl:element>
    </xsl:if>
    <xsl:if test="contains(@styleCode,'Italics') and contains(@styleCode,'Underline') and not (contains(@styleCode, 'Bold'))">
      <xsl:element name="i">
        <xsl:element name="u">
          <xsl:apply-templates/>
        </xsl:element>
      </xsl:element>
    </xsl:if>
    <xsl:if test="contains(@styleCode,'Italics') and contains(@styleCode,'Underline') and contains(@styleCode, 'Bold')">
      <xsl:element name="b">
        <xsl:element name="i">
          <xsl:element name="u">
            <xsl:apply-templates/>
          </xsl:element>
        </xsl:element>
      </xsl:element>
    </xsl:if>
    <xsl:if test="not (contains(@styleCode,'Italics') or contains(@styleCode,'Underline') or contains(@styleCode, 'Bold'))">
      <xsl:apply-templates/>
    </xsl:if>
  </xsl:template>
  <!--    Superscript or Subscript   -->
  <xsl:template match="n1:sup">
    <xsl:element name="sup">
      <xsl:apply-templates/>
    </xsl:element>
  </xsl:template>
  <xsl:template match="n1:sub">
    <xsl:element name="sub">
      <xsl:apply-templates/>
    </xsl:element>
  </xsl:template>
  <!-- 16.06.19 추가 -->
  <!-- linkHtml -->
  <xsl:template match="n1:linkHtml">
    <xsl:choose>
      <!-- 06.28 PACS ICON 관련 부분 추가-->
      <xsl:when test="contains(./@ID, 'PACS')">
        <xsl:call-template name="addPacsIcon">
          <xsl:with-param name="href" select="./@href"/>
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:variable name="linkTargetContainsJavaScript">
          <xsl:call-template name="containsJavaScript">
            <xsl:with-param name="reference" select="./@href"></xsl:with-param>
          </xsl:call-template>
        </xsl:variable>
        <xsl:if test="$linkTargetContainsJavaScript != 'true'">
          <xsl:if test="string-length(./@href) &gt; 0 or
                          string-length(./text()) &gt; 0">
            <xsl:variable name="linkText">
              <xsl:choose>
                <xsl:when test="string-length(./text()) &gt; 0">
                  <xsl:value-of select="./text()"/>
                </xsl:when>
                <xsl:otherwise>
                  <xsl:value-of select="concat('link:',./@href)"/>
                </xsl:otherwise>
              </xsl:choose>
            </xsl:variable>
            <xsl:variable name="linkTarget">
              <xsl:choose>
                <xsl:when test="string-length(./@href) &gt; 1 and
                            starts-with(./@href, '#')">
                  <xsl:variable name="referenceId" select="substring-after(./@href, '#')"/>
                  <xsl:variable name="attachmentReference">
                    <xsl:choose>
                      <xsl:when test="//n1:observationMedia/@ID=$referenceId">true</xsl:when>
                      <xsl:otherwise>false</xsl:otherwise>
                    </xsl:choose>
                  </xsl:variable>
                  <xsl:choose>
                    <xsl:when test="$attachmentReference='true'">
                      <xsl:if test="//n1:observationMedia/@ID=$referenceId">
                        <xsl:value-of select="//n1:observationMedia[@ID=$referenceId]/n1:value/n1:reference/@value"/>
                      </xsl:if>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:value-of select="concat('#', $referenceId)"/>
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:when>
                <xsl:when test="not(starts-with(./@href, '#')) and
                            string-length(./@href) &gt; 0">
                  <xsl:value-of select="./@href"/>
                </xsl:when>
              </xsl:choose>
            </xsl:variable>
            <xsl:choose>
              <xsl:when test="string-length($linkTarget)=0">
                <xsl:element name="a">
                  <xsl:value-of select="$linkText"/>
                </xsl:element>
              </xsl:when>
              <xsl:otherwise>
                <xsl:element name="a">
                  <xsl:attribute name="href">
                    <xsl:value-of select="$linkTarget"/>
                  </xsl:attribute>
                  <xsl:value-of select="$linkText"/>
                </xsl:element>
              </xsl:otherwise>
            </xsl:choose>
          </xsl:if>
        </xsl:if>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <xsl:template name="containsJavaScript">
    <xsl:param name="reference"/>
    <xsl:variable name="upperCaseReference">
      <xsl:call-template name="caseUp">
        <xsl:with-param name="data" select="$reference"/>
      </xsl:call-template>
    </xsl:variable>
    <xsl:choose>
      <xsl:when test="contains($upperCaseReference, 'JAVASCRIPT') or contains($upperCaseReference, 'JSCRIPT')">true</xsl:when>
      <xsl:otherwise>false</xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- 06.28 PACS ICON 관련 부분 추가-->
  <!-- 10.14 PACS ICON 관련 부분 수정 : class명 수정-->
  <xsl:template name="addPacsIcon">
    <xsl:param name="href"/>
    <xsl:element name="a">
      <!--<xsl:attribute name="href">
        <xsl:value-of select="./@href"/>
      </xsl:attribute>-->
      <xsl:attribute name="onclick">
        javascript: popupView('<xsl:value-of select="./@href"/>'); return false;
      </xsl:attribute>
      <!--<xsl:attribute name="target">_blank</xsl:attribute>-->
      <xsl:element name="img">
        <xsl:attribute name="width">50</xsl:attribute>
        <xsl:attribute name="height">20</xsl:attribute>
        <xsl:attribute name="class">main_section_button</xsl:attribute>
        <xsl:attribute name="src">
          data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADoAAAAUCAYAAADcHS5uAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAABGFJREFUeNrcV9srvGsUXjPGmd84bqfIhXM5JsSFK8QmaeeCKxHu9l/AlXLhVtnlQm7cUC6IKJKcDzkTyvkwjkVjnI39Pqver+H3zZ6pfTNZ9TXfe/zWs9aznvcdzefnJ6Wlpf3h6uo6bzabfegHmVarNb28vKSK1ysdOjQajaG0tFTr5ub2k3DS6+urvqen5+Tj4yNQJ7JZFBISYvTx8dHTDzMkLjw8/OXw8PBPZNTdyclJI1A7lJNbW1u0vr5OFxcXSp+npyfFxcWh1MjDw8OufQQ2LTAydVGnjgIUvqyurjKohoYGBibt7u6OhoeHaXp6mrKyssjd3d2u/WAK0Pf3d4cAenZ2RsHBwVRXV8d+7e/v0/39PQmxpLCwMCovL6eoqCjq7e2l1NRUm/sJgbUNtKCggJKTk5X23NwcjY+PK22MYU57eztH29pajPX19TENMzMzKTc3l/vR7u7upufnZ2Xd29sb1dbW0u3tLW1sbNDV1ZWSlb29PYqPj6ekpCQ6Pj7m9b6+vv8/o4jG2NgYzczMcJQrKys5wgcHBzweExPDIKKjo3mOtLKyMnagra2NxxF5IQrk7e3N4GV/YWEhBQYGKvvBUlJS+HdtbY0MBsMXf5BZ0NrZ2ZmD1dXVZZOJdgFFP8Bi7PT0lM7Pz9lZtIVS8zMyMkI5OTk0MTHBaxISEjgora2tyj4LCwv8CxHBHjc3N9zu7+//7ZuoSaGSdHl5qeqT0Wik7e1tZoafnx8zwB7qamUDC74/6IdI4T0iIoKBnZyccBsOgVqgM2Qc4+gPCgrifrX9dnd3eY+ioiLVcTwuLi6cOdDS2hzQWZyRXLfW5lhiUIDKjH5/MCk/P5+ampqoqqqKhoaGuDYwlp6ezjTC+/z8PGdSrpEs+P5cX19zpjGOPcEEtTleXl6cdbU9QGcosjwl1OZYPr9RV40CcGhgYIDr1NJQZ5D2mpoape/p6Ymp+PDwwNKvRkspNhAgjNfX1/P82dlZZfzo6IjFBplfXFzk+gdj4AsUWVzpOKgoJQTFXuraFCOZHUtLTEyk0dHRL2AaGxspIyODpqamGCiOh87OTnp8fKTs7OwvZx7WotbwXdDPcn8ACA0NpeLiYtxVOciSggBYUVHBwHGZQO2jD/PsEiM1MN/FSBpuJFDGlpaWL/2Tk5OsrjjQQU841NzczGOge0dHB79XV1dTSUkJv+/s7LCIWe4DNV5aWuJvoGwQJBw1oGtAQICiGcg6ggI9QNatgZUZ1Qjn/tLr9R2xsbG/HOkKiDqNjIwkf39/0ul07DBYADVGxkFt2ODgIG1ubjJYNRNnr0kE6m+HuxlZZnZlZYWpDSVGJlEGMAgSMo5jLC8vT1FYu85RR7vUSwM4CVCayWRioYIoIrOgOG5etoDei2yaHRXofwVgeXmZgYDm1vwX2DDwoMFEcSYaBA2CITTiT/iP+C8KXAiGoLlRvP+Sqpssivwf0ZEr6fwDzCySNiEw1aLxrwADAGsuee+PNkkCAAAAAElFTkSuQmCC
        </xsl:attribute>
      </xsl:element>
    </xsl:element>
  </xsl:template>

  <!-- 05.19 추가 -->
  <!-- 10.14 수정 -->
  <!-- ##################### Header ##################### -->
  <!-- header - fix -->
  <xsl:template name="header-fix">
    <div class="header">
      <div class="fix">
        <h1>
          <xsl:value-of select="$title"/>
        </h1>
        <p class="date">
          작성일
          <strong>
            <xsl:call-template name="getDate2">
              <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:effectiveTime"/>
            </xsl:call-template>
          </strong>
          <br/>          
        </p>
      </div>
    </div>
  </xsl:template>
  <!-- informantionRecipient-custom -->
  <xsl:template name="informationRecipient_custom">
    <xsl:if test="count(/n1:ClinicalDocument/n1:informationRecipient) &gt; 0">
      <div>
        <h3 class="cnt">
          <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:receivedOrganization/n1:asOrganizationPartOf/n1:wholeOrganization/n1:name"/>&#160;
          <span>
            <xsl:call-template name="show-name-kr">
              <xsl:with-param name="name" select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:informationRecipient/n1:name"/>
            </xsl:call-template> 선생님 귀하
          </span>
          <xsl:choose>
            <xsl:when test="/n1:ClinicalDocument/n1:code/@code = '57133-1'">
              <span style="float: right; font-size: 12px; font-style: normal;">
                의뢰번호 : <xsl:value-of select="/n1:ClinicalDocument/n1:id/@extension"/>
              </span>
            </xsl:when>
            <xsl:when test="/n1:ClinicalDocument/n1:code/@code = '18761-7'">
              <span style="float: right; font-size: 12px; font-style: normal;">
                회송번호 : <xsl:value-of select="/n1:ClinicalDocument/n1:id/@extension"/>
              </span>
            </xsl:when>
            <xsl:otherwise></xsl:otherwise>
          </xsl:choose>          
        </h3>                
      </div>
    </xsl:if>
  </xsl:template>
  <!-- recordTarget custom -->
  <xsl:template name="recordTarget_custom">
    <div>
      <ul class="patientInfo">
        <li class="name">
          <xsl:call-template name="show-name-kr">
            <xsl:with-param name="name" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:name"/>
          </xsl:call-template> / <xsl:call-template name="show-gender-kr">
            <xsl:with-param name="gender" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:administrativeGenderCode"/>
          </xsl:call-template>
        </li>
        <li class="birth">
          Birth. <xsl:call-template name="getDate">
            <xsl:with-param name="date" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:patient/n1:birthTime"/>
          </xsl:call-template>
        </li>
        <li class="tel">
          Tel. <xsl:value-of select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:telecom/@value"/>
        </li>
        <br></br>
        <li class="addr" style="margin-left: 0px;">
          Addr.&#160;
          <xsl:call-template name="show-address-kr">
            <xsl:with-param name="address" select="/n1:ClinicalDocument/n1:recordTarget/n1:patientRole/n1:addr"/>
          </xsl:call-template>
        </li>
      </ul>      
    </div>
  </xsl:template>
  <!-- organization_information -->
  <xsl:template name="organization_information">
    <xsl:choose>
      <!-- 1. 의뢰서 또는 CRS에서만 생성기관 정보 표시-->
      <!-- 2. 회신서 / 회송서 일 경우는 의뢰기관 정보 안보이도록 처리 -->
      <!-- 의뢰서 : 57133-1 / 회신서 : 11488-4 / 회송서 : 18761-7 / CRS : 34133-9 / 판독소견서 : 18748-4 -->
      <!-- 의뢰서 -->
      <xsl:when test="/n1:ClinicalDocument/n1:code/@code = '57133-1'">
        <table class="contents_table">
          <colgroup>
            <col width="20%" />
            <col width="20%" />
            <col width="15%" />
            <col width="45%" />
          </colgroup>
          <tbody>
            <tr>
              <th>의뢰병원</th>
              <td style="text-align:center">
                <xsl:value-of select="/n1:ClinicalDocument/n1:custodian/n1:assignedCustodian/n1:representedCustodianOrganization/n1:name"/>
              </td>
              <td style="text-align: center;">
                <xsl:call-template name="show-name-kr">
                  <xsl:with-param name="name" select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:informationRecipient/n1:name"/>
                </xsl:call-template>
              </td>              
              <td>
                <xsl:call-template name="show-address-kr">
                  <xsl:with-param name="address" select="/n1:ClinicalDocument/n1:custodian/n1:assignedCustodian/n1:representedCustodianOrganization/n1:addr"/>
                </xsl:call-template>
              </td>
            </tr>
            <!--<xsl:if test="count(/n1:ClinicalDocument/n1:informationRecipient) &gt; 0">
              <tr>
                <th>협진병원</th>
                <td style="text-align:center">
                  <xsl:value-of select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:receivedOrganization/n1:asOrganizationPartOf/n1:wholeOrganization/n1:name"/>
                </td>
                <td>
                  <xsl:call-template name="show-address-kr">
                    <xsl:with-param name="address" select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:addr"/>
                  </xsl:call-template>
                </td>
              </tr>
            </xsl:if>-->
          </tbody>
        </table>
      </xsl:when>
      <!-- 회송서 / 회신서-->
      <xsl:when test="/n1:ClinicalDocument/n1:code/@code = '18761-7' or /n1:ClinicalDocument/n1:code/@code = '11488-4'">
        <table class="contents_table">
          <colgroup>
            <col width="20%" />
            <col width="20%" />
            <col width="15%" />
            <col width="45%" />
          </colgroup>
          <tbody>
            <xsl:if test="count(/n1:ClinicalDocument/n1:informationRecipient) &gt; 0">
              <xsl:for-each select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient">
                <tr>
                  <th>협진병원</th>
                  <td style="text-align:center">
                    <xsl:value-of select="n1:receivedOrganization/n1:asOrganizationPartOf/n1:wholeOrganization/n1:name"/>
                  </td>
                  <td>
                    <xsl:call-template name="show-name-kr">
                      <xsl:with-param name="name" select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient/n1:informationRecipient/n1:name"/>
                    </xsl:call-template>
                  </td>
                  <td>
                    <xsl:call-template name="show-address-kr">
                      <xsl:with-param name="address" select="n1:addr"/>
                    </xsl:call-template>
                  </td>
                </tr>
              </xsl:for-each>
            </xsl:if>
          </tbody>
        </table>
      </xsl:when>
      <!--판독소견서 / CRS -->
      <xsl:otherwise>
        <table class="contents_table">
          <colgroup>
            <col width="15%" />
            <col width="20%" />
            <col width="65%" />
          </colgroup>
          <tbody>
            <tr>
              <th>요양기관</th>
              <td style="text-align:center">
                <xsl:value-of select="/n1:ClinicalDocument/n1:custodian/n1:assignedCustodian/n1:representedCustodianOrganization/n1:name"/>
              </td>
              <td>
                <xsl:call-template name="show-address-kr">
                  <xsl:with-param name="address" select="/n1:ClinicalDocument/n1:custodian/n1:assignedCustodian/n1:representedCustodianOrganization/n1:addr"/>
                </xsl:call-template>
              </td>
            </tr>
            <xsl:if test="count(/n1:ClinicalDocument/n1:informationRecipient) &gt; 0">
              <xsl:for-each select="/n1:ClinicalDocument/n1:informationRecipient/n1:intendedRecipient">
                <tr>
                  <th>수신기관</th>
                  <!-- for-each -->
                  <td style="text-align:center">
                    <xsl:value-of select="n1:receivedOrganization/n1:asOrganizationPartOf/n1:wholeOrganization/n1:name"/>
                  </td>
                  <td>
                    <xsl:call-template name="show-address-kr">
                      <xsl:with-param name="address" select="n1:addr"/>
                    </xsl:call-template>
                  </td>
                </tr>
              </xsl:for-each>
            </xsl:if>
          </tbody>
        </table>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- /##################### Header ##################### -->

  <!-- ##################### Hyper Link ##################### -->
  <xsl:template name="make-hyperlink">
    <h3 style="margin-top:15px;">바로가기</h3>
    <xsl:for-each select="n1:component/n1:structuredBody/n1:component/n1:section/n1:title">
      <xsl:variable name="position" select="position()"/>
      <xsl:if test="$position mod 3 = '1'">
        <ul style="width: 33%;">
          <li>
            <strong>
              <a href="#{generate-id(.)}" style="color:#222;">
                <xsl:value-of select="."/>
              </a>
            </strong>
          </li>
        </ul>
      </xsl:if>
      <xsl:if test="$position mod 3 = '2'">
        <ul style="width: 33%;">
          <li>
            <strong>
              <a href="#{generate-id(.)}" style="color:#222;">
                <xsl:value-of select="."/>
              </a>
            </strong>
          </li>
        </ul>
      </xsl:if>
      <xsl:if test="$position mod 3 = '0'">
        <ul style="width:33%;">
          <li style="align : right;">
            <strong>
              <a href="#{generate-id(.)}" style="color:#222;">
                <xsl:value-of select="."/>
              </a>
            </strong>
          </li>
        </ul>
        <br/>
      </xsl:if>
    </xsl:for-each>
    <!--<div class="head_content">      
    </div>-->
  </xsl:template>
  <!-- ##################### Data format ##################### -->
  <!-- show-gender-kr  -->
  <xsl:template name="show-gender-kr">
    <xsl:param name="gender"/>
    <xsl:choose>
      <xsl:when test="$gender/@code   = &apos;M&apos;">
        <xsl:text>남</xsl:text>
      </xsl:when>
      <xsl:when test="$gender/@code  = &apos;F&apos;">
        <xsl:text>여</xsl:text>
      </xsl:when>
      <xsl:when test="$gender/@code  = &apos;U&apos;">
        <xsl:text>식별불가</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>
  <!-- show date ( format : yyyy-MM-dd )-->
  <xsl:template name="getDate">
    <xsl:param name="date"/>
    <xsl:choose>
      <xsl:when test="$date/@value">
        <xsl:value-of select="concat(substring($date/@value,1,4),'-',substring($date/@value,5,2),'-',substring($date/@value,7,2))"/>
        <xsl:text> </xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$date"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- show date ( format : yyyy.MM.dd )-->
  <xsl:template name="getDate2">
    <xsl:param name="date"/>
    <xsl:choose>
      <xsl:when test="$date/@value">
        <xsl:value-of select="concat(substring($date/@value,1,4),'.',substring($date/@value,5,2),'.',substring($date/@value,7,2),'.')"/>
        <xsl:text> </xsl:text>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$date"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- show-name-kr  -->
  <xsl:template name="show-name-kr">
    <xsl:param name="name"/>
    <xsl:choose>
      <xsl:when test="$name/n1:family">
        <xsl:if test="$name/n1:prefix">
          <xsl:value-of select="$name/n1:prefix"/>
          <xsl:text> </xsl:text>
        </xsl:if>
        <xsl:value-of select="$name/n1:family"/>
        <!--<xsl:text> </xsl:text>-->
        <xsl:value-of select="$name/n1:given"/>
        <xsl:if test="$name/n1:suffix">
          <xsl:text>, </xsl:text>
          <xsl:value-of select="$name/n1:suffix"/>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$name"/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  <!-- show-telecom-kr -->
  <xsl:template name="show-telecom-kr">
    <xsl:param name="telecom"/>
    <xsl:choose>
      <xsl:when test="$telecom">
        <xsl:variable name="type" select="substring-before($telecom/@value, ':')"/>
        <xsl:variable name="value" select="substring-after($telecom/@value, ':')"/>
        <xsl:if test="$type">
          <xsl:call-template name="translateTelecomCode">
            <xsl:with-param name="code" select="$type"/>
          </xsl:call-template>
          <xsl:if test="@use">
            <xsl:text> (</xsl:text>
            <xsl:call-template name="translateTelecomCode">
              <xsl:with-param name="code" select="@use"/>
            </xsl:call-template>
            <xsl:text>)</xsl:text>
          </xsl:if>
          <xsl:text>: </xsl:text>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$value"/>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$telecom/@value"/>
      </xsl:otherwise>
    </xsl:choose>
    <br/>
  </xsl:template>
  <!-- show-address-kr-->
  <xsl:template name="show-address-kr">
    <xsl:param name="address"/>
    <xsl:choose>
      <xsl:when test="$address">
        <xsl:if test="$address/@use">
          <xsl:text> </xsl:text>
          <xsl:call-template name="translateTelecomCode">
            <xsl:with-param name="code" select="$address/@use"/>
          </xsl:call-template>
          <xsl:text>:</xsl:text>
          <br/>
        </xsl:if>
        <xsl:if test="string-length($address/n1:country)>0">
          <xsl:value-of select="$address/n1:country"/>
          <xsl:text> </xsl:text>
        </xsl:if>
        <xsl:if test="string-length($address/n1:state)>0">
          <xsl:value-of select="$address/n1:state"/>
          <xsl:text> </xsl:text>
        </xsl:if>
        <xsl:if test="string-length($address/n1:city)>0">
          <xsl:value-of select="$address/n1:city"/>
          <xsl:text> </xsl:text>
        </xsl:if>
        <xsl:if test="string-length($address/n1:additionalLocator)>0">
          <xsl:value-of select="$address/n1:additionalLocator"/>
          <xsl:text> </xsl:text>
        </xsl:if>
        <xsl:for-each select="$address/n1:streetAddressLine">
          <xsl:value-of select="."/>
          <xsl:text> </xsl:text>
        </xsl:for-each>
        <xsl:if test="$address/n1:streetName">
          <xsl:value-of select="$address/n1:streetName"/>
          <xsl:text> </xsl:text>
          <xsl:value-of select="$address/n1:houseNumber"/>
        </xsl:if>
        <xsl:if test="string-length($address/n1:postalCode)>0">
          <xsl:text>&#160;</xsl:text>
          <xsl:value-of select="$address/n1:postalCode"/>
        </xsl:if>
      </xsl:when>
      <xsl:otherwise>
        <xsl:text>유효하지 않은 주소정보</xsl:text>
      </xsl:otherwise>
    </xsl:choose>
    <br/>
  </xsl:template>
  <!-- 상단 문구-->
  <xsl:template name="addComment">
    <xsl:choose>
      <!-- 진료회신서 / 진료회송서 의 경우 해당문구 출력.-->
      <!-- 회신서 문구 : 선생님께서 의뢰해주신 환자의 진료결과를 다음과 같이 알려드리오니 향후 진료에 참고하시기 바랍니다.귀원의 무궁한 발전을 기원합니다. -->
      <!-- 회송서 문구 : 회송 환자의 진료결과를 다음과 같이 알려드리오니 향후 진료에 참고하시기 바랍니다. -->
      <!-- 진료회송서 -->
      <xsl:when test="/n1:ClinicalDocument/n1:code/@code = '18761-7'"> 
        <p>회송 환자의 진료결과를 다음과 같이 알려드리오니 향후 진료에 참고하시기 바랍니다.</p>
        <p>귀원의 무궁한 발전을 기원합니다.</p>
        <br/>
      </xsl:when>
      <!-- 진료회신서 -->
      <xsl:when test="/n1:ClinicalDocument/n1:code/@code = '11488-4'">        
        <p>선생님께서 의뢰해주신 환자의 진료결과를 다음과 같이 알려드리오니 향후 진료에 참고하시기 바랍니다.</p>
        <p>귀원의 무궁한 발전을 기원합니다.</p>
        <br/>
      </xsl:when>      
      <xsl:otherwise></xsl:otherwise>

    </xsl:choose>
  </xsl:template>
  <!-- 하단 문구 -->
  <xsl:template name="addBottomComment">
    <!--회신서 / 회송서 하단 문구-->
    <xsl:choose>
      <xsl:when test="/n1:ClinicalDocument/n1:code/@code = '18761-7' or /n1:ClinicalDocument/n1:code/@code ='11488-4'">
        <div class ="main_content" style="padding : 15px 17px">
          <br/>
          <h3 style="text-align : center;">
            향후 귀병원에서 치료를 계속하실 계획이 있으시거나 더 궁금하신 사항은<br/>
            전화(031-787-2020) 또는 팩스(031-787-4035) 로 연락주시면 곧 회답하여 드리겠습니다.
          </h3>
          <br/>
          <table border="0">
            <tbody>
              <tr>
                <td width="50%"></td>
                <td width="10%">
                  <h6>진료과</h6>
                </td>
                <td width="20%">
                  <xsl:value-of select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:representedOrganization/n1:name"/>
                </td>
                <td width="10%">
                  <h6>진료의</h6>
                </td>
                <td width="10%">
                  <xsl:value-of select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedPerson/n1:name/n1:family"/>
                  <xsl:value-of select="/n1:ClinicalDocument/n1:author/n1:assignedAuthor/n1:assignedPerson/n1:name/n1:given"/>
                </td>
              </tr>
              <tr>
                <td></td>
                <td colspan="2">
                  <h6>분당서울대학교병원 진료협력센터</h6>
                </td>
                <td>
                  <h6>담당자</h6>
                </td>
                <td>
                  <xsl:value-of select="/n1:ClinicalDocument/n1:participant[@typeCode='AUT']/n1:associatedEntity/n1:associatedPerson/n1:name/n1:family"/>
                  <xsl:value-of select="/n1:ClinicalDocument/n1:participant[@typeCode='AUT']/n1:associatedEntity/n1:associatedPerson/n1:name/n1:given"/>
                </td>
              </tr>
            </tbody>
          </table>
          <br/>
          <h3 style="text-align : center;">
            이 기록은 의무기록에 근거하여 작성되었습니다.
          </h3>
          <br/>
          <p style="text-align : right;">
            문의전화 : 031-787-2020 (분당서울대학교병원 진료협력센터)
          </p>
        </div>
      </xsl:when>
      <xsl:otherwise></xsl:otherwise>
    </xsl:choose>

  </xsl:template>
</xsl:stylesheet>
