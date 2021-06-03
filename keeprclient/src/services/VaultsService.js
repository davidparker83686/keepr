import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
// import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    AppState.vaults.push(res.data)
    // this.getVaultsByUserId()
  }

  async getVaultsByUserId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.vaults = res.data
  }

  async deleteVault(id) {
    await api.delete(`api/vaults/${id}`)
    AppState.vaults = AppState.vaults.filter(i => i.id !== id)
    AppState.activeVault = {}
    // this.getAll()
  }

  async getVaultById(id) {
    const res = await api.get(`api/vaults/${id}`)
    AppState.activeVault = res.data
  }

  async activeVault(vault) {
    logger.log(vault)
    await api.get('api/vaults/' + vault.id, vault)
    AppState.activeVault = vault
  }
}

export const vaultsService = new VaultsService()
