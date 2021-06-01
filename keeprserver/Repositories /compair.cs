





// internal IEnumerable<PartyPartyMemberViewModel> GetPartiesByProfileId(string id)
// {
//   string sql = @"
//       SELECT
//       party.*,
//       pm.id AS PartyMemberId,
//       pr.*
//       FROM partymembers pm
//       JOIN parties party ON pm.partyId = party.id
//       JOIN profiles pr ON pr.id = party.creatorId
//       WHERE pm.memberId = @id;";

//   return _db.Query<PartyPartyMemberViewModel, Profile, PartyPartyMemberViewModel>(sql, (party, profile) =>
//   {
//     party.Creator = profile;
//     return party;
//   }, new { id }, splitOn: "id");
// }