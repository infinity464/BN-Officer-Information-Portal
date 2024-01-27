using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois.ApplicationService.Interface
{
    public interface IReportService
    {
        Task<bool> ExecutePlanForBroadSheetForeignCourseMissionVisit(int nominationId);
        Task<bool> ExecutePlanPromotionBoard(int promotionBoardId);
    }
}
