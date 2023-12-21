/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
using System.Collections.Generic;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.WIN;

public partial class PadelCourtWindow
{
    public PadelCourtWindow(List<PadelCourt> padelCourts)
    {
        InitializeComponent();
        DataContext = this;
        PadelCourts = padelCourts;
    }

    public List<PadelCourt> PadelCourts { get; set; }
}