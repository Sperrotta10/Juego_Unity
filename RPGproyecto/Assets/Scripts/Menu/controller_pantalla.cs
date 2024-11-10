using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class controller_pantalla : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown resolucionesDropDown;
    Resolution[] resoluciones;
    List<Resolution> resolucionesFiltradas; // Lista para almacenar resoluciones únicas

    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        RevisarResolucion();
    }

    public void ActiveFULLS(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionesDropDown.ClearOptions();
        
        List<string> opciones = new List<string>();
        resolucionesFiltradas = new List<Resolution>(); // Inicializamos la lista de resoluciones únicas
        HashSet<string> resolucionesUnicas = new HashSet<string>(); // HashSet para evitar duplicados
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;

            // Solo agregamos la resolución si no está en el HashSet
            if (resolucionesUnicas.Add(opcion))
            {
                opciones.Add(opcion);
                resolucionesFiltradas.Add(resoluciones[i]); // Guardamos la resolución original
            }

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = resolucionesFiltradas.Count - 1; // Índice de la resolución actual
            }
        }

        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();

        resolucionesDropDown.value = PlayerPrefs.GetInt("numeroResolucion", resolucionActual);
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolucionesDropDown.value);

        // Usamos resolucionesFiltradas para obtener la resolución correcta
        Resolution resolution = resolucionesFiltradas[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
