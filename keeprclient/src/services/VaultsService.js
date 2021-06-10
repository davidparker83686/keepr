import { AppState } from '../AppState'

import { api } from './AxiosService'
import Notification from '../utils/Notification'
import { logger } from '../utils/Logger'

class VaultsService {
  async createVault(newVault) {
    const res = await api.post('api/vaults', newVault)
    AppState.vaults.push(res.data)
    Notification.toast('Successfully Created Vault', 'success')
    // this.getVaultsByUserId()
  }

  async getVaultsByUserId(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    AppState.usersVaults = res.data
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

  async activeVault(id) {
    const res = await api.get(`api/vaults/${id}`)
    // await api.get('api/vaults/' + vault.id, vault)
    AppState.activeVault = res.data
    logger.log(AppState.activeVault)
    logger.log(res.data)
  }

  async getKeepsByVaultId(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    return res.data
  }

  async getVaultKeepsById(id) {
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.vaultKeeps = res.data
  }
}

// function fizbuzz(number) {
//   for (i = 0, i <= number.length, i++ ) {
//     if (i % 3 == 0 && i % 5 == 0) {
//       return "fizbuzz"
//     } else if (i % 3 == 0) {
//       return "fizz"
//     }else if(i%5==0)
//   }

// }

export const vaultsService = new VaultsService()
