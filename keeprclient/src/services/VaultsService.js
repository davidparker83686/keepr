import { AppState } from '../AppState'
// import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async createVault(newVault) {
    newVault.location = AppState.account.location
    const res = await api.post('api/vaults', newVault)
    AppState.vaults.push(res.data)
    // this.getVaultsByUserId()
  }

  async getVaultsByUserId(id) {
    const res = await api.get(`api/profile/${id}/vaults`)
    AppState.vaults = res.data
  }

  async deleteVault(id) {
    await api.delete(`api/vaults/${id}`)
    AppState.vaults = AppState.vaults.filter(i => i.id !== id)
    this.getAll()
  }
}

export const vaultsService = new VaultsService()
