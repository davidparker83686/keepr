import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
    logger.log(res.data)
  }

  async createKeep(newKeep) {
    const res = await api.post('api/keeps', newKeep)
    AppState.keeps.push(res.data)
    // this.getKeepsByUserId()
  }

  async getKeepsByUserId(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    AppState.keeps = res.data
  }

  async deleteKeep(id) {
    await api.delete(`api/keeps/${id}`)
    AppState.keeps = AppState.keeps.filter(i => i.id !== id)
    AppState.activeKeep = {}
  }

  async activeKeep(keep) {
    logger.log(keep)
    AppState.activeKeep = keep

    await api.put('api/keeps/' + keep.id, keep)
    const viewedKeep = AppState.keeps.findIndex(k => k.id === keep.id)
    keep.views = keep.views + 1
    AppState.keeps[viewedKeep] = keep
  }

  async addToVault(id1, id2) {
    const newVaultKeep = await api.get(`api/keeps/${id1}`)
    const res = await api.post('api/vaultkeeps', newVaultKeep)
    res.vaultId = id2
    AppState.vaultkeeps.push(res.data)
  }
}

export const keepsService = new KeepsService()
