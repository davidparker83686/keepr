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
    // AppState.accountVaults = res.data
  }

  async getVaultsByProfile(id, id2) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.accountVaults = res.data
    if (id !== id2) {
      AppState.accountVaults = AppState.accountVaults.filter(v => v.isPrivate !== true)
    }
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

  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    return res.data
  }
}

export const vaultsService = new VaultsService()
