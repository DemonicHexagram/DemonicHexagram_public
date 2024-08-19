using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;

public class CSVReader
{
    static string _split_re = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string _line_split_re = @"\r\n|\n\r|\n|\r";
    static char[] _trim_chars = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        // 똑같은 자료형을 반환하기 위해서 리스트 형태 자료형
        var list = new List<Dictionary<string, object>>();

        //이부분은 주소를 찾는 함수 여기를 수정해야한다. 지금 상태는
        //리소스 폴더 안에서 그 파일을 로드하는 경우이다.
        TextAsset date = Resources.Load(file) as TextAsset;

        //스플리트 Object-> Regex을 상속 받은 객체입니다.
        string[] lines = Regex.Split(date.text, _line_split_re);

        // 길이가 0또는 1이하일때 리턴해준다.
        if (lines.Length <= 1)
        {
            return list;
        }

        // 이름을 의미합니다.
        string[] header = Regex.Split(lines[0], _split_re);
        
        // 여기서 보자면 줄을 체크하면 라인을 체크
        for (int i = 1; i < lines.Length; i++)
        {
            // 라인을 체크하고 난뒤
            var values = Regex.Split(lines[i], _split_re);
            // 방어코드로
            // 0일 때 없을 경우에 건넌 뛰는 것을 의미합니다.
            if (values.Length == 0 || values[0] == KeyWordManager.str_nullTxt) continue;

            // 그리고 넣을 것들 이제 만듭니다.
            var entry = new Dictionary<string, object>();
            
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {

                string value = values[j];
                
                value = value.TrimStart(_trim_chars).TrimEnd(_trim_chars).Replace("\\n", "\n");
                object finalvalue = value;
                
                // 찾기위해서
                // 정수인가?
                int n;
                // 부동소수점인가?
                float f;

                // 정수이면 체크하는 조건문
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                // 부동소수점 체크하는 조건문
                else if(float.TryParse(value, out f)) 
                {
                    finalvalue = f;
                }

                entry[header[j]] =finalvalue;
            }
            // 추가하고
            list.Add(entry);
        }
        // 반환합니다.
        return list;
    }
}
