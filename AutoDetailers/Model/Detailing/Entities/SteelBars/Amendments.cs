namespace Model.Detailing.Entities.SteelBars
{
    class Amendments
    {
        public static int GetNumberOfAmendments(double length, int amendmentLength)
        {
            if (length == 1200) { return 0; }
            else
            {
                int numberOfAmendment = (int)(length / 1200);
                if ((length + numberOfAmendment * amendmentLength) % 1200 == 0)
                {
                    return numberOfAmendment;
                }
                else
                {
                    length += numberOfAmendment * amendmentLength;
                    numberOfAmendment = (int)(length / 1200);
                    return numberOfAmendment;
                }
            }
        }
    }
}
